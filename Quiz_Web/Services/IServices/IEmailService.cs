namespace Quiz_Web.Services.IServices
{
	public interface IEmailService
	{
		Task<bool> SendPasswordResetEmail(string toEmail, string resetLink);
	}
}
