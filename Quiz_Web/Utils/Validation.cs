using System.Text.RegularExpressions;

namespace Quiz_Web.Utils
{
	public class Validation
	{
		public static bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email)) return false;
			string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
			return Regex.IsMatch(email, pattern);
		}

		public static bool IsValidUsername(string username)
		{
			return !string.IsNullOrWhiteSpace(username) && username.Length >= 3 && username.Length <= 100;
		}

		public static bool IsValidPassword(string password)
		{
			if (string.IsNullOrWhiteSpace(password) || password.Length < 8) return false;
			bool hasUpper = password.Any(char.IsUpper);
			bool hasLower = password.Any(char.IsLower);
			bool hasDigit = password.Any(char.IsDigit);
			bool hasSpecial = Regex.IsMatch(password, @"[\W_]");
			return hasUpper && hasLower && hasDigit && hasSpecial;
		}

		public static bool IsValidFullName(string fullName)
		{
			return !string.IsNullOrWhiteSpace(fullName) && fullName.Trim().Length <= 200;
		}

		public static bool IsValidPhone(string phone)
		{
			if (string.IsNullOrWhiteSpace(phone)) return true;
			string pattern = @"^[\d\s\-\+\(\)]+$";
			return Regex.IsMatch(phone, pattern) && phone.Length <= 20;
		}

		public static bool IsValidTitle(string title)
		{
			return !string.IsNullOrWhiteSpace(title) && title.Trim().Length <= 200;
		}

		public static bool IsValidSlug(string slug)
		{
			if (string.IsNullOrWhiteSpace(slug)) return false;
			string pattern = @"^[a-z0-9-]+$";
			return Regex.IsMatch(slug, pattern) && slug.Length <= 200;
		}

		public static bool IsValidPrice(decimal? price)
		{
			return price == null || price >= 0;
		}

		public static bool IsValidUrl(string url)
		{
			if (string.IsNullOrWhiteSpace(url)) return true;
			return Uri.TryCreate(url, UriKind.Absolute, out _);
		}

		public static bool IsValidTimeLimit(int? timeLimit)
		{
			return timeLimit == null || (timeLimit >= 1 && timeLimit <= 1440);
		}

		public static bool IsValidPoints(decimal points)
		{
			return points >= 0.1m && points <= 100m;
		}

		public static bool IsValidMaxAttempts(int? maxAttempts)
		{
			return maxAttempts == null || (maxAttempts >= 1 && maxAttempts <= 10);
		}
	}
}
