using NUnit.Framework;
using OnlineShop.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework.Legacy;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace OnlineShop.Tests;
[TestFixture()]
public class RedisCacheTest
{
	private IRedisCache _cache;

	[SetUp]
	public void SetUp()
	{
		var factory = new WebApplicationFactory<Program>();
		var scope = factory.Services.CreateScope();
		_cache = scope.ServiceProvider.GetRequiredService<IRedisCache>();
	}

	[Test]
	public void SaveAndGetInt()
	{
		_cache.SaveVerificationCode("test", 1234);
		var answer = _cache.GetVerificationCode("test");
		ClassicAssert.AreEqual(1234, answer.Result.Value);
	}

	[Test]
	public void SaveAndWaitFiveMinutes()
	{
		_cache.SaveVerificationCode("test", 1234);
		var t = Task.Run(async () =>
		{
			await Task.Delay(310000);
			return _cache.GetVerificationCode("test");
		});
		t.Wait();
		var expected = Result.Failure<int>("Verification Code Not Found");
		ClassicAssert.AreEqual(expected, t.Result.Result);
	}
}
