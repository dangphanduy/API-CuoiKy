using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_Web.Models.EF;
using Quiz_Web.Models.Entities;
using Quiz_Web.Helper;
using Quiz_Web.Utils;
using Quiz_Web.Services.IServices;

namespace Quiz_Web.Controllers
{
	[Authorize(Roles = "Admin,Teacher")]
	public class AdminController : Controller
	{
		private readonly LearningPlatformContext _context;
		private readonly IDashboardService _dashboardService;

		public AdminController(LearningPlatformContext context, IDashboardService dashboardService)
		{
			_context = context;
			_dashboardService = dashboardService;
		}

		[Route("/admin")]
		public IActionResult Index()
		{
			var data = _dashboardService.GetOverviewData();
			return View(data);
		}

		// DASHBOARD ACTIONS
		public IActionResult UserAnalytics()
		{
			var data = _dashboardService.GetUserAnalytics();
			return View(data);
		}

		public IActionResult LearningActivities()
		{
			var data = _dashboardService.GetLearningActivities();
			return View(data);
		}

		public IActionResult RevenuePayments()
		{
			var data = _dashboardService.GetRevenuePayments();
			return View(data);
		}

		public IActionResult LearningResults()
		{
			var data = _dashboardService.GetLearningResults();
			return View(data);
		}

		// DASHBOARD API ENDPOINTS
		[HttpGet]
		public JsonResult GetOverviewData()
		{
			var data = _dashboardService.GetOverviewData();
			return Json(data);
		}

		[HttpGet]
		public JsonResult GetUserAnalyticsData()
		{
			var data = _dashboardService.GetUserAnalytics();
			return Json(data);
		}

		[HttpGet]
		public JsonResult GetLearningActivitiesData()
		{
			var data = _dashboardService.GetLearningActivities();
			return Json(data);
		}

		[HttpGet]
		public JsonResult GetRevenuePaymentsData()
		{
			var data = _dashboardService.GetRevenuePayments();
			return Json(data);
		}

		[HttpGet]
		public JsonResult GetLearningResultsData()
		{
			var data = _dashboardService.GetLearningResults();
			return Json(data);
		}

		// REPORTS
		public async Task<IActionResult> UserReports()
		{
			var data = new
			{
				TotalUsers = await _context.Users.CountAsync(),
				ActiveUsers = await _context.Users.Where(u => u.Status == 1).CountAsync(),
				NewUsersThisMonth = await _context.Users.Where(u => u.CreatedAt >= DateTime.UtcNow.AddDays(-30)).CountAsync(),
				UsersByRole = await _context.Users.Include(u => u.Role).GroupBy(u => u.Role.Name).Select(g => new { Role = g.Key, Count = g.Count() }).ToListAsync()
			};
			return View(data);
		}

		public async Task<IActionResult> CourseReports()
		{
			var data = new
			{
				TotalCourses = await _context.Courses.CountAsync(),
				PublishedCourses = await _context.Courses.Where(c => c.IsPublished).CountAsync(),
				TotalPurchases = await _context.CoursePurchases.Where(p => p.Status == "Paid").CountAsync(),
				PopularCourses = await _context.CoursePurchases.Where(p => p.Status == "Paid").GroupBy(p => p.Course.Title).Select(g => new { Course = g.Key, Purchases = g.Count() }).OrderByDescending(x => x.Purchases).Take(10).ToListAsync()
			};
			return View(data);
		}

		public async Task<IActionResult> TestReports()
		{
			// ✅ Xử lý null-safe và đảm bảo luôn trả về dữ liệu hợp lệ
			var totalTests = await _context.Tests.CountAsync();
			var totalAttempts = await _context.TestAttempts.CountAsync();

			var recentAttempts = await _context.TestAttempts
				.Include(a => a.User)
				.Include(a => a.Test)
				.OrderByDescending(a => a.StartedAt)
				.Take(15)
				.Select(a => new
				{
					FullName = a.User.FullName ?? "Unknown",
					Title = a.Test.Title ?? "Unknown Test",
					Score = a.Score,
					MaxScore = a.MaxScore,
					StartedAt = a.StartedAt
				})
				.ToListAsync();

			var topScores = await _context.TestAttempts
				.Include(a => a.User)
				.Include(a => a.Test)
				.Where(a => a.Score.HasValue && a.MaxScore.HasValue && a.MaxScore > 0)
				.OrderByDescending(a => (decimal)a.Score!.Value / a.MaxScore!.Value)
				.Take(15)
				.Select(a => new
				{
					FullName = a.User.FullName ?? "Unknown",
					Title = a.Test.Title ?? "Unknown Test",
					Score = a.Score!.Value,
					MaxScore = a.MaxScore!.Value,
					Percentage = (decimal)a.Score!.Value / a.MaxScore!.Value * 100
				})
				.ToListAsync();

			var data = new
			{
				TotalTests = totalTests,
				TotalAttempts = totalAttempts,
				RecentAttempts = recentAttempts,
				TopScores = topScores
			};

			return View(data);
		}

		public async Task<IActionResult> RevenueReports()
		{
			// ✅ DEBUG: In ra log để kiểm tra dữ liệu
			var allPurchases = await _context.CoursePurchases.ToListAsync();
			var paidPurchases = await _context.CoursePurchases.Where(p => p.Status == "Paid").ToListAsync();
			
			Console.WriteLine($"=== REVENUE REPORTS DEBUG ===");
			Console.WriteLine($"Total CoursePurchases: {allPurchases.Count}");
			Console.WriteLine($"Paid CoursePurchases: {paidPurchases.Count}");
			Console.WriteLine($"Statuses: {string.Join(", ", allPurchases.Select(p => p.Status).Distinct())}");

			// ✅ Tính tổng doanh thu từ CoursePurchases (100%)
			var totalGrossRevenue = await _context.CoursePurchases
				.Where(p => p.Status == "Paid")
				.SumAsync(p => (decimal?)p.PricePaid) ?? 0;

			var monthlyGrossRevenue = await _context.CoursePurchases
				.Where(p => p.Status == "Paid" && p.PurchasedAt >= DateTime.UtcNow.AddDays(-30))
				.SumAsync(p => (decimal?)p.PricePaid) ?? 0;

			Console.WriteLine($"Total Gross Revenue: {totalGrossRevenue}");
			Console.WriteLine($"Monthly Gross Revenue: {monthlyGrossRevenue}");

			// ✅ Tính 40% cho admin
			var totalRevenue = totalGrossRevenue * 0.40m;
			var monthlyRevenue = monthlyGrossRevenue * 0.40m;

			// ✅ Lấy giao dịch gần đây với null-safe
			var recentPurchases = await _context.CoursePurchases
				.Include(p => p.Buyer)
				.Include(p => p.Course)
				.Where(p => p.Status == "Paid")
				.OrderByDescending(p => p.PurchasedAt)
				.Take(15)
				.Select(p => new { 
					FullName = p.Buyer != null ? p.Buyer.FullName : "Unknown",
					Title = p.Course != null ? p.Course.Title : "Unknown Course",
					PricePaid = p.PricePaid,
					PurchasedAt = p.PurchasedAt
				})
				.ToListAsync();

			Console.WriteLine($"Recent Purchases Count: {recentPurchases.Count}");

			// ✅ TopPayments từ Payments table
			var topPayments = await _context.Payments
				.Where(p => p.Status == "Paid")
				.OrderByDescending(p => p.Amount)
				.Take(10)
				.Select(p => new { p.Amount, p.PaidAt })
				.ToListAsync();

			Console.WriteLine($"Top Payments Count: {topPayments.Count}");
			Console.WriteLine($"=== END DEBUG ===");

			var data = new
			{
				TotalRevenue = totalRevenue, // 40% cho admin
				MonthlyRevenue = monthlyRevenue, // 40% cho admin
				RecentPurchases = recentPurchases,
				TopPayments = topPayments
			};

			return View(data);
		}

		// PROFILE & SETTINGS
		public async Task<IActionResult> Profile()
		{
			var userId = GetCurrentUserId();
			var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == userId);
			if (user == null) return NotFound();
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Profile(User user)
		{
			var currentUserId = GetCurrentUserId();
			if (user.UserId != currentUserId) return Forbid();

			// Validation
			if (string.IsNullOrWhiteSpace(user.Username) || user.Username.Length > 100)
			{
				TempData["Error"] = "Username is required and cannot exceed 100 characters";
				return View(user);
			}

			if (string.IsNullOrWhiteSpace(user.FullName) || user.FullName.Length > 200)
			{
				TempData["Error"] = "Full name is required and cannot exceed 200 characters";
				return View(user);
			}

			if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
			{
				TempData["Error"] = "Valid email is required";
				return View(user);
			}

			if (await _context.Users.AnyAsync(u => u.Username == user.Username && u.UserId != user.UserId))
			{
				TempData["Error"] = "Username already exists";
				return View(user);
			}

			if (await _context.Users.AnyAsync(u => u.Email == user.Email && u.UserId != user.UserId))
			{
				TempData["Error"] = "Email already exists";
				return View(user);
			}

			var existingUser = await _context.Users.FindAsync(user.UserId);
			if (existingUser == null) return NotFound();

			existingUser.Username = user.Username.ToLower().Trim();
			existingUser.FullName = user.FullName.Trim();
			existingUser.Email = user.Email.ToLower().Trim();
			existingUser.Phone = user.Phone;

			await _context.SaveChangesAsync();
			TempData["Success"] = "Profile updated successfully";
			return View(existingUser);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
		{
			if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
			{
				TempData["Error"] = "All password fields are required";
				return RedirectToAction("Profile");
			}

			if (newPassword != confirmPassword)
			{
				TempData["Error"] = "New passwords do not match";
				return RedirectToAction("Profile");
			}

			if (newPassword.Length < 8)
			{
				TempData["Error"] = "Password must be at least 8 characters";
				return RedirectToAction("Profile");
			}

			var userId = GetCurrentUserId();
			var user = await _context.Users.FindAsync(userId);
			if (user == null) return NotFound();

			// Verify current password
			if (user.PasswordHash != HashHelper.ComputeHash(currentPassword))
			{
				TempData["Error"] = "Current password is incorrect";
				return RedirectToAction("Profile");
			}

			user.PasswordHash = HashHelper.ComputeHash(newPassword);
			await _context.SaveChangesAsync();
			TempData["Success"] = "Password changed successfully";
			return RedirectToAction("Profile");
		}

		public IActionResult Settings()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateTheme(string theme)
		{
			// Store theme preference in session/cookie
			Response.Cookies.Append("Theme", theme, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
			TempData["Success"] = "Theme updated successfully";
			return RedirectToAction("Settings");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateLanguage(string language, string dateFormat, string timeZone)
		{
			// Store language preferences in session/cookie
			Response.Cookies.Append("Language", language, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
			Response.Cookies.Append("DateFormat", dateFormat, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
			Response.Cookies.Append("TimeZone", timeZone, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
			TempData["Success"] = "Language settings updated successfully";
			return RedirectToAction("Settings");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdatePreferences(bool emailNotifications, bool soundNotifications, bool autoSave, bool showTips)
		{
			// Store preferences in session/cookie
			Response.Cookies.Append("EmailNotifications", emailNotifications.ToString(), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
			Response.Cookies.Append("SoundNotifications", soundNotifications.ToString(), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
			Response.Cookies.Append("AutoSave", autoSave.ToString(), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
			Response.Cookies.Append("ShowTips", showTips.ToString(), new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
			TempData["Success"] = "Preferences updated successfully";
			return RedirectToAction("Settings");
		}

		private int GetCurrentUserId()
		{
			var username = User.Identity?.Name;
			var user = _context.Users.FirstOrDefault(u => u.Username == username);
			return user?.UserId ?? 1;
		}
	}
}
