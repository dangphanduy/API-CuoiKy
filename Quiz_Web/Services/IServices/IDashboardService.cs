using Quiz_Web.Models.ViewModels;

namespace Quiz_Web.Services.IServices
{
    public interface IDashboardService
    {
        DashboardOverviewViewModel GetOverviewData();
        UserAnalyticsViewModel GetUserAnalytics();
        LearningActivitiesViewModel GetLearningActivities();
        RevenuePaymentsViewModel GetRevenuePayments();
        LearningResultsViewModel GetLearningResults();
        SystemActivityViewModel GetSystemActivity();
    }
}