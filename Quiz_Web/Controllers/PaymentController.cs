using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz_Web.Models.EF;
using Quiz_Web.Models.Entities;
using Quiz_Web.Models.MoMoPayment;
using Quiz_Web.Services.IServices;
using System.Security.Claims;

namespace Quiz_Web.Controllers
{
	public class PaymentController : Controller
	{
		private readonly IMoMoPaymentService _momoService;
		private readonly ICartService _cartService;
		private readonly IPurchaseService _purchaseService;
		private readonly LearningPlatformContext _context;
		private readonly ILogger<PaymentController> _logger;

		public PaymentController(
			IMoMoPaymentService momoService,
			ICartService cartService,
			IPurchaseService purchaseService,
			LearningPlatformContext context,
			ILogger<PaymentController> logger)
		{
			_momoService = momoService;
			_cartService = cartService;
			_purchaseService = purchaseService;
			_context = context;
			_logger = logger;
		}

		private int GetCurrentUserId()
		{
			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
			{
				throw new UnauthorizedAccessException("User not authenticated");
			}
			return userId;
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateMoMoPayment()
		{
			var userId = GetCurrentUserId();
			var cartItems = await _cartService.GetCartItemsAsync(userId);

			if (!cartItems.Any())
				return Json(new { success = false, message = "Giỏ hàng trống" });

			var total = cartItems.Sum(x => x.Course.Price);

			// 1) Tạo Order
			var order = new Order
			{
				BuyerId = userId,
				TotalAmount = total,
				Status = "Pending"
			};

			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			// 2) Tạo OrderItems
			foreach (var item in cartItems)
			{
				_logger.LogInformation($"Adding OrderItem: CourseId={item.CourseId}, Price={item.Course.Price}");
				_context.OrderItems.Add(new OrderItem
				{
					OrderId = order.OrderId,
					CourseId = item.CourseId,
					Price = item.Course.Price
				});
			}
			await _context.SaveChangesAsync();

			// ⭐ 3) Tạo Purchase (Pending) - CHỈ TẠO 1 LẦN DUY NHẤT Ở ĐÂY
			foreach (var item in cartItems)
			{
				_context.CoursePurchases.Add(new CoursePurchase
				{
					BuyerId = userId,
					CourseId = item.CourseId,
					PricePaid = item.Course.Price,
					Currency = "VND",
					Status = "Pending",
					PurchasedAt = DateTime.UtcNow
				});
			}
			await _context.SaveChangesAsync();

			// 4) Tạo Payment
			var orderIdStr = $"ORDER_{order.OrderId}_{DateTime.Now:yyyyMMddHHmmss}";
			var payment = new Payment
			{
				OrderId = order.OrderId,
				Provider = "MoMo",
				Amount = total,
				Currency = "VND",
				Status = "Pending",
				RawPayload = orderIdStr
			};

			_context.Payments.Add(payment);
			await _context.SaveChangesAsync();

			// 5) Gọi MoMo
			var momo = await _momoService.CreatePaymentAsync(total, "Thanh toán giỏ hàng", orderIdStr);

			if (momo.resultCode == 0)
			{
				payment.ProviderRef = momo.orderId;
				await _context.SaveChangesAsync();

				return Json(new
				{
					success = true,
					payUrl = momo.payUrl,
					orderId = orderIdStr
				});
			}

			return Json(new { success = false, message = momo.message });
		}

		// ✅ IPN Callback - MoMo gọi tự động
		[HttpPost("Payment/MoMoCallback")]
		[AllowAnonymous]
		public async Task<IActionResult> MoMoCallback([FromBody] MoMoIpnRequest ipn)
		{
			_logger.LogInformation($"MoMo IPN received: orderId={ipn.orderId}, resultCode={ipn.resultCode}");

			try
			{
				if (!_momoService.ValidateSignature(ipn))
				{
					_logger.LogWarning("Invalid MoMo signature");
					return BadRequest("Invalid signature");
				}

				var payment = await _context.Payments
					.Include(p => p.Order)
					.FirstOrDefaultAsync(p => p.ProviderRef == ipn.orderId);

				if (payment == null)
				{
					_logger.LogWarning($"Payment not found for orderId: {ipn.orderId}");
					return NotFound("Payment not found");
				}

				// Chỉ xử lý nếu chưa được xử lý
				if (payment.Status != "Pending")
				{
					_logger.LogInformation($"Payment already processed: {payment.Status}");
					return Ok("Already processed");
				}

				var order = payment.Order;

				if (ipn.resultCode == 0)
				{
					// ✅ THANH TOÁN THÀNH CÔNG
					await CompletePaymentAsync(payment, order, ipn.transId.ToString());
					_logger.LogInformation($"Payment completed successfully for order {order.OrderId}");
				}
				else
				{
					// ❌ THANH TOÁN THẤT BẠI
					await FailPaymentAsync(payment, order);
					_logger.LogWarning($"Payment failed for order {order.OrderId}, resultCode: {ipn.resultCode}");
				}

				return Ok("IPN Processed");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error processing MoMo IPN");
				return StatusCode(500, "Internal error");
			}
		}

		// ✅ Return URL - User quay lại sau khi thanh toán
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> MoMoReturn(string orderId, int resultCode, string message)
		{
			_logger.LogInformation($"MoMo Return: orderId={orderId}, resultCode={resultCode}");

			try
			{
				var payment = await _context.Payments
					.Include(p => p.Order)
					.FirstOrDefaultAsync(p => p.ProviderRef == orderId);

				if (payment == null)
				{
					ViewBag.Success = false;
					ViewBag.Message = "Không tìm thấy giao dịch.";
					return View("PaymentResult");
				}

				// ✅ NẾU ĐÃ XỬ LÝ XONG (từ IPN)
				if (payment.Status == "Paid")
				{
					ViewBag.Success = true;
					ViewBag.Message = "Thanh toán thành công!";
					ViewBag.OrderId = payment.Order.OrderId;
					return View("PaymentResult");
				}

				if (payment.Status == "Failed")
				{
					ViewBag.Success = false;
					ViewBag.Message = "Thanh toán thất bại: " + message;
					return View("PaymentResult");
				}

				// ✅ NẾU IPN CHƯA VỀ - VERIFY LẠI VỚI MOMO
				if (resultCode == 0 && payment.Status == "Pending")
				{
					var queryResult = await _momoService.QueryTransactionAsync(orderId);

					if (queryResult != null && queryResult.resultCode == 0)
					{
						// Xác thực thành công → Cập nhật ngay
						await CompletePaymentAsync(payment, payment.Order, queryResult.transId.ToString());

						ViewBag.Success = true;
						ViewBag.Message = "Thanh toán thành công!";
						ViewBag.OrderId = payment.Order.OrderId;
						return View("PaymentResult");
					}
				}

				// Trường hợp còn lại
				ViewBag.Success = resultCode == 0;
				ViewBag.Message = resultCode == 0
					? "Thanh toán đang được xử lý. Vui lòng đợi."
					: "Thanh toán thất bại: " + message;
				return View("PaymentResult");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error processing MoMo return");
				ViewBag.Success = false;
				ViewBag.Message = "Có lỗi xảy ra khi xử lý thanh toán.";
				return View("PaymentResult");
			}
		}

		// ✅ API để check status (cho frontend polling)
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> CheckPaymentStatus(string orderId)
		{
			try
			{
				var payment = await _context.Payments
					.Include(p => p.Order)
					.FirstOrDefaultAsync(p => p.RawPayload == orderId);

				if (payment == null)
					return Json(new { status = "NOT_FOUND" });

				return Json(new
				{
					status = payment.Status.ToUpper(),
					orderStatus = payment.Order?.Status,
					message = payment.Status == "Paid" ? "Thanh toán thành công" : ""
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error checking payment status");
				return Json(new { status = "ERROR", message = ex.Message });
			}
		}

		// ✅ HELPER: Complete Payment - UPDATE PENDING PURCHASES
		private async Task CompletePaymentAsync(Payment payment, Order order, string transactionId)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();

			try
			{
				// 1. Cập nhật Payment
				payment.Status = "Paid";
				payment.PaidAt = DateTime.UtcNow;
				payment.ProviderRef = transactionId;

				// 2. Cập nhật Order
				order.Status = "Paid";

				// 3. Lấy danh sách OrderItems
				var orderItems = await _context.OrderItems
					.Where(i => i.OrderId == order.OrderId)
					.ToListAsync();

				_logger.LogInformation($"Processing {orderItems.Count} order items for order {order.OrderId}");

				// 4. ⭐ UPDATE PENDING PURCHASES (KHÔNG TẠO MỚI!)
				foreach (var item in orderItems)
				{
					var purchase = await _context.CoursePurchases
						.FirstOrDefaultAsync(x =>
							x.BuyerId == order.BuyerId &&
							x.CourseId == item.CourseId &&
							x.Status == "Pending");

					if (purchase != null)
					{
						_logger.LogInformation($"Updating purchase: CourseId={item.CourseId}, Old Status=Pending");

						purchase.Status = "Paid";
						purchase.PurchasedAt = DateTime.UtcNow;

						_logger.LogInformation($"Updated purchase: CourseId={item.CourseId}, New Status=Paid");
					}
					else
					{
						_logger.LogWarning($"No pending purchase found for CourseId={item.CourseId}, UserId={order.BuyerId}");
					}
				}

				// 5. Xóa giỏ hàng
				await _cartService.ClearCartAsync(order.BuyerId);

				// 6. Commit transaction
				await _context.SaveChangesAsync();
				await transaction.CommitAsync();

				_logger.LogInformation($"Payment completed successfully for order {order.OrderId}");
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				_logger.LogError(ex, $"Error completing payment for order {order.OrderId}");
				throw;
			}
		}

		// ✅ HELPER: Fail Payment
		private async Task FailPaymentAsync(Payment payment, Order order)
		{
			payment.Status = "Failed";
			order.Status = "Failed";

			// Chuyển tất cả Purchase Pending → Failed
			var purchases = await _context.CoursePurchases
				.Where(x => x.BuyerId == order.BuyerId && x.Status == "Pending")
				.ToListAsync();

			foreach (var p in purchases)
			{
				p.Status = "Failed";
			}

			await _context.SaveChangesAsync();
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> CheckCourseAccess(int courseId)
		{
			try
			{
				var userId = GetCurrentUserId();
				var hasAccess = await _purchaseService.HasUserPurchasedCourseAsync(userId, courseId);
				return Json(new { hasAccess });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error checking course access");
				return Json(new { hasAccess = false });
			}
		}
	}
}