using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OnlineShop.Core.Interfaces;
using System.Security.Cryptography;

namespace OnlineShop.Infrastructure.Jwt
{
	public class JwtProvider : IJwtProvider
	{
		private readonly JwtOptions _jwtOptions;
		public JwtProvider(IOptions<JwtOptions> options)
		{
			_jwtOptions = options.Value;
		}

		public string GenerateToken(string email, string role)
		{
			Claim[] claims = [new("role", role), new("email", email)];
			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				claims: claims,
				signingCredentials: signingCredentials,
				expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiredHours));
			var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenValue;
		}

		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[64];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		public string GetClaim(string token, string claim)
		{
			var jsonToken = new JwtSecurityTokenHandler().ReadToken(token);
			var decoddedToken = jsonToken as JwtSecurityToken;
			var result = decoddedToken.Claims.First(x => x.Type == claim).Value;
			return result;
		}
	}
}
