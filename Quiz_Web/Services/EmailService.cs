using Quiz_Web.Services.IServices;
using System.Net;
using System.Net.Mail;

namespace Quiz_Web.Services
{
	public class EmailService : IEmailService
	{
		private readonly IConfiguration _configuration;

		public EmailService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<bool> SendPasswordResetEmail(string toEmail, string resetLink)
		{
			try
			{
				var smtpHost = _configuration["EmailSettings:SmtpHost"];
				var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
				var fromEmail = _configuration["EmailSettings:FromEmail"];
				var fromPassword = _configuration["EmailSettings:FromPassword"];
				var fromName = _configuration["EmailSettings:FromName"];

				var mailMessage = new MailMessage
				{
					From = new MailAddress(fromEmail, fromName),
					Subject = "Đặt lại mật khẩu",
					Body = $@"
						<html>
						<body>
							<h2>Yêu cầu đặt lại mật khẩu</h2>
							<p>Bạn đã yêu cầu đặt lại mật khẩu cho tài khoản của mình.</p>
							<p>Vui lòng nhấp vào liên kết bên dưới để đặt lại mật khẩu:</p>
							<p><a href='{resetLink}'>Đặt lại mật khẩu</a></p>
							<p>Liên kết này sẽ hết hạn sau 1 giờ.</p>
							<p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>
						</body>
						</html>
					",
					IsBodyHtml = true
				};

				mailMessage.To.Add(toEmail);

				using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
				{
					smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
					smtpClient.EnableSsl = true;
					await smtpClient.SendMailAsync(mailMessage);
				}
				return true;

			}catch(Exception ex)
			{
				Console.WriteLine($"Lỗi gửi mailL: {ex.Message}");
				return false;
			}
		}
	}
}
