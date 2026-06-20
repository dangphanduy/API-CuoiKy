using Microsoft.EntityFrameworkCore;
using Quiz_Web.Models.EF;

namespace Quiz_Web.Services
{
    public class SoftDeleteCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SoftDeleteCleanupService> _logger;
        private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(24); // Run daily
        private readonly int _retentionDays = 30; // Configurable retention days, default 30 days

        public SoftDeleteCleanupService(IServiceScopeFactory scopeFactory, ILogger<SoftDeleteCleanupService> logger, IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;

            if (int.TryParse(configuration["SoftDeleteRetentionDays"], out int days))
            {
                _retentionDays = days;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SoftDeleteCleanupService is starting with retention: {RetentionDays} days.", _retentionDays);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DoCleanupAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during soft delete cleanup execution.");
                }

                try
                {
                    await Task.Delay(_cleanupInterval, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    // Ignore task cancellation during delay
                }
            }

            _logger.LogInformation("SoftDeleteCleanupService is stopping.");
        }

        private async Task DoCleanupAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LearningPlatformContext>();
                var thresholdDate = DateTime.UtcNow.AddDays(-_retentionDays);

                // Fetch tests marked as deleted whose UpdatedAt is older than thresholdDate
                var expiredTests = await context.Tests
                    .Include(t => t.Questions)
                        .ThenInclude(q => q.QuestionOptions)
                    .Include(t => t.TestAttempts)
                        .ThenInclude(ta => ta.AttemptAnswers)
                    .Where(t => t.IsDeleted && t.UpdatedAt.HasValue && t.UpdatedAt.Value < thresholdDate)
                    .ToListAsync(stoppingToken);

                if (expiredTests.Any())
                {
                    _logger.LogInformation("Found {Count} expired soft-deleted tests to permanently delete.", expiredTests.Count);

                    foreach (var test in expiredTests)
                    {
                        // Cascade delete child entities due to database foreign keys
                        foreach (var question in test.Questions)
                        {
                            context.QuestionOptions.RemoveRange(question.QuestionOptions);
                        }
                        context.Questions.RemoveRange(test.Questions);

                        foreach (var attempt in test.TestAttempts)
                        {
                            context.AttemptAnswers.RemoveRange(attempt.AttemptAnswers);
                        }
                        context.TestAttempts.RemoveRange(test.TestAttempts);

                        context.Tests.Remove(test);
                    }

                    await context.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("Successfully purged {Count} expired tests from database.", expiredTests.Count);
                }
                else
                {
                    _logger.LogInformation("No expired soft-deleted tests found for permanent deletion.");
                }
            }
        }
    }
}
