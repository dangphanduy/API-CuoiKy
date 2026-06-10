namespace Quiz_Web.Models.ViewModels
{
    public class DashboardOverviewViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalCourses { get; set; }
        public int TotalTests { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ChartDataPoint> UserGrowthData { get; set; } = new();
        public List<ChartDataPoint> RevenueData { get; set; } = new();
    }

    public class UserAnalyticsViewModel
    {
        public List<PieChartData> UsersByRole { get; set; } = new();
        public List<ChartDataPoint> NewUsersPerMonth { get; set; } = new();
        public List<ChartDataPoint> ActiveUsersData { get; set; } = new();
        public int TotalActiveUsers { get; set; }
        public int NewUsersThisMonth { get; set; }
    }

    public class LearningActivitiesViewModel
    {
        public List<ChartDataPoint> TestsCompletedData { get; set; } = new();
        public List<ChartDataPoint> CoursesEnrolledData { get; set; } = new();
        public List<BarChartData> PopularCourses { get; set; } = new();
        public int TotalTestsCompleted { get; set; }
        public int TotalEnrollments { get; set; }
    }

    public class RevenuePaymentsViewModel
    {
        public List<ChartDataPoint> MonthlyRevenue { get; set; } = new();
        public List<PieChartData> RevenueBySource { get; set; } = new();
        public List<BarChartData> TopSellingCourses { get; set; } = new();
        public decimal TotalRevenue { get; set; }
        public decimal MonthlyGrowth { get; set; }
    }

    public class LearningResultsViewModel
    {
        public List<BarChartData> TestScores { get; set; } = new();
        public List<PieChartData> CertificatesByType { get; set; } = new();
        public List<TableSummaryData> TopPerformers { get; set; } = new();
        public double AverageScore { get; set; }
        public int TotalCertificates { get; set; }
    }

    public class SystemActivityViewModel
    {
        public List<BarChartData> LoginActivity { get; set; } = new();
        public List<TableSummaryData> SystemLogs { get; set; } = new();
        public List<ChartDataPoint> ErrorRates { get; set; } = new();
        public int TotalLogins { get; set; }
        public int SystemErrors { get; set; }
    }

    public class ChartDataPoint
    {
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }

    public class PieChartData
    {
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Color { get; set; } = string.Empty;
    }

    public class BarChartData
    {
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Category { get; set; } = string.Empty;
    }

    public class TableSummaryData
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}