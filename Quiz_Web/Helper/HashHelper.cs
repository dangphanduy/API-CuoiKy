using System.Text;

namespace Quiz_Web.Helper
{
	public class HashHelper
	{
		public static string ComputeHash(string input)
		{
			using (var sha256 = System.Security.Cryptography.SHA256.Create())
			{
				var bytes = Encoding.UTF8.GetBytes(input);
				var hashBytes = sha256.ComputeHash(bytes);
				return Convert.ToBase64String(hashBytes);
			}
		}
	}
}
