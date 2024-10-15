using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;
using OnlineShop.Core.Interfaces;

namespace OnlineShop.DataBase.Redis
{
	public class RedisCache : IRedisCache
	{
		IDistributedCache cache;
		public RedisCache(IDistributedCache distributedCache)
		{
			cache = distributedCache;
		}

		public async Task<Result<int>> GetVerificationCode(string email)
		{
			var verificationString = await cache.GetStringAsync(email);

			if (verificationString == null)
				return Result.Failure<int>("Verification Code Not Found");

			int verificationCode;
			var parseResult = int.TryParse(verificationString, out verificationCode);

			if (!parseResult)
				return Result.Failure<int>("Code parsing error");
			return Result.Success(verificationCode);
		}

		public async Task SaveVerificationCode(string email, int verificationCode)
		{
			var verificationString = verificationCode.ToString();

			await cache.SetStringAsync(email, verificationString, new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
			});
		}
	}
}
