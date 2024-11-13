using Microsoft.AspNetCore.Identity;

namespace T_HosCase.Entities
{
	public class User
	{
		public int UserId { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string HashPassword { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public bool Status { get; set; }
	}
}
