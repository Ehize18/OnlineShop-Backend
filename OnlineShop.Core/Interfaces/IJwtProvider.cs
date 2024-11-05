namespace OnlineShop.Core.Interfaces;

public interface IJwtProvider
{
	string GenerateToken(string email, string role);
	string GenerateRefreshToken();
	string GetClaim(string token, string claim);
}