using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz_Web.Models.ViewModels;
using Quiz_Web.Services.IServices;
using System.Security.Claims;

namespace Quiz_Web.Controllers
{
	[Route("reviews")]
	public class ReviewController : Controller
	{
		private readonly IReviewService _reviewService;
		private readonly ICourseService _courseService;
		private readonly ILogger<ReviewController> _logger;

		public ReviewController(
			IReviewService reviewService,
			ICourseService courseService,
			ILogger<ReviewController> logger)
		{
			_reviewService = reviewService;
			_courseService = courseService;
			_logger = logger;
		}

		// POST: /reviews/create
		[Authorize]
		[HttpPost("create")]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CreateReviewViewModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData["Error"] = "Vui lòng ki?m tra l?i thông tin ?ánh giá.";
				return RedirectToAction("Detail", "Course", new { id = model.CourseId });
			}

			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
			{
				return Challenge();
			}

			// Check if user can review
			if (!_reviewService.CanUserReview(model.CourseId, userId))
			{
				TempData["Error"] = "B?n c?n mua khóa h?c tr??c khi ?ánh giá ho?c b?n ?ã ?ánh giá r?i.";
				return RedirectToAction("Detail", "Course", new { id = model.CourseId });
			}

			var review = _reviewService.CreateReview(model, userId);

			if (review == null)
			{
				TempData["Error"] = "Có l?i x?y ra khi t?o ?ánh giá.";
				return RedirectToAction("Detail", "Course", new { id = model.CourseId });
			}

			TempData["Success"] = "C?m ?n b?n ?ã ?ánh giá khóa h?c!";
			
			// Get course to redirect with slug
			var course = _courseService.GetCourseById(model.CourseId);
			if (course != null)
			{
				return RedirectToAction("Detail", "Course", new { slug = course.Slug });
			}

			return RedirectToAction("Index", "Course");
		}

		// GET: /reviews/edit/{id}
		[Authorize]
		[HttpGet("edit/{id:int}")]
		public IActionResult Edit(int id)
		{
			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
			{
				return Challenge();
			}

			var review = _reviewService.GetReviewById(id);
			if (review == null)
			{
				TempData["Error"] = "Không tìm th?y ?ánh giá.";
				return RedirectToAction("Index", "Course");
			}

			// Check ownership
			if (review.UserId != userId)
			{
				TempData["Error"] = "B?n không có quy?n ch?nh s?a ?ánh giá này.";
				return RedirectToAction("Detail", "Course", new { slug = review.Course.Slug });
			}

			var viewModel = new EditReviewViewModel
			{
				ReviewId = review.ReviewId,
				Rating = review.Rating,
				Comment = review.Comment
			};

			ViewBag.Course = review.Course;
			return View(viewModel);
		}

		// POST: /reviews/edit/{id}
		[Authorize]
		[HttpPost("edit/{id:int}")]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, EditReviewViewModel model)
		{
			if (id != model.ReviewId)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				var review = _reviewService.GetReviewById(id);
				if (review != null)
				{
					ViewBag.Course = review.Course;
				}
				return View(model);
			}

			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
			{
				return Challenge();
			}

			var updatedReview = _reviewService.UpdateReview(model, userId);

			if (updatedReview == null)
			{
				TempData["Error"] = "Không th? c?p nh?t ?ánh giá.";
				return View(model);
			}

			TempData["Success"] = "C?p nh?t ?ánh giá thành công!";
			return RedirectToAction("Detail", "Course", new { slug = updatedReview.Course.Slug });
		}

		// POST: /reviews/delete/{id}
		[Authorize]
		[HttpPost("delete/{id:int}")]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
			{
				return Challenge();
			}

			// Get review to get course slug before deletion
			var review = _reviewService.GetReviewById(id);
			string? courseSlug = review?.Course?.Slug;

			var success = _reviewService.DeleteReview(id, userId);

			if (!success)
			{
				TempData["Error"] = "Không th? xóa ?ánh giá.";
			}
			else
			{
				TempData["Success"] = "?ã xóa ?ánh giá.";
			}

			if (!string.IsNullOrEmpty(courseSlug))
			{
				return RedirectToAction("Detail", "Course", new { slug = courseSlug });
			}

			return RedirectToAction("Index", "Course");
		}

		// GET: /reviews/rating-stats/{courseId}
		[HttpGet("rating-stats/{courseId:int}")]
		public IActionResult GetRatingStats(int courseId)
		{
			var distribution = _reviewService.GetRatingDistribution(courseId);
			var totalReviews = distribution.Values.Sum();

			var stats = new
			{
				distribution = distribution,
				totalReviews = totalReviews,
				percentages = distribution.ToDictionary(
					kvp => kvp.Key,
					kvp => totalReviews > 0 ? Math.Round((double)kvp.Value / totalReviews * 100, 1) : 0
				)
			};

			return Json(stats);
		}
	}
}
