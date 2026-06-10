namespace Quiz_Web.Models.ViewModels
{
    public class CreateUserViewModel
    {
        public string Username { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public string? Phone { get; set; }
    }

    public class EditUserViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RoleId { get; set; }
        public string? Phone { get; set; }
        public int Status { get; set; }
    }
}