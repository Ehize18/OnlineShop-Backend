using System.Security.Claims;

namespace OnlineShop.Core.Models
{
	public class User
	{
		public int? Id { get; }
		public string Email { get; } = string.Empty;
		public string Role { get; } = string.Empty;
		public string RefreshToken { get; } = string.Empty;
		public DateTime CreatedAt { get; }
		public DateTime UpdatedAt { get; }

		public User() { }

		public User(string email, string role, string refreshToken)
		{
			Email = email;
			Role = role;
			RefreshToken = refreshToken;
			var now = DateTime.UtcNow;
			CreatedAt = now;
			UpdatedAt = now;
		}

		public User(int id, string email, string role, string refreshToken,  DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			Email = email;
			Role = role;
			RefreshToken = refreshToken;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}
	}
}
