namespace OnlineShop.Infrastructure.Jwt
{
	public class JwtOptions
	{
		public string SecretKey { get; set; } = string.Empty;
		public int ExpiredHours { get; set; }
	}
}
