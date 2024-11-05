using CSharpFunctionalExtensions;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;
using System.Runtime.CompilerServices;

namespace OnlineShop.Application.Services
{
	public class LoginService : ILoginService
	{
		private readonly IRedisCache _cache;
		private readonly IEmailService _emailService;
		private readonly IJwtProvider _jwtProvider;
		private readonly IUsersRepository _usersRepository;

		public LoginService(IRedisCache redisCache, IEmailService emailService, IJwtProvider jwtProvider, IUsersRepository usersRepository)
		{
			_cache = redisCache;
			_emailService = emailService;
			_jwtProvider = jwtProvider;
			_usersRepository = usersRepository;
		}

		public async Task LoginRequest(string email)
		{
			var random = new Random();
			var randomInt = random.Next(99999999);
			var verificationCode = randomInt.ToString();
			if (verificationCode.Length < 8)
				verificationCode = string.Concat(Enumerable.Repeat("0", 8 - verificationCode.Length)) + verificationCode;
			await _cache.SaveVerificationCode(email, verificationCode);
			_emailService.SendMail(email, "Our Verification Code", verificationCode);
		}

		public async Task<Result<List<string>>> LoginConfirm(string email, int otp)
		{
			var otpSavedResult = await _cache.GetVerificationCode(email);
			if (otpSavedResult.IsFailure)
			{
				return Result.Failure<List<string>>(otpSavedResult.Error);
			}
			var otpFromCache = otpSavedResult.Value;
			if (otpFromCache != otp)
			{
				return Result.Failure<List<string>>("Incorrect code");
			}
			var refreshToken = string.Empty;
			var role = string.Empty;
			var userResult = await _usersRepository.GetUserByEmail(email);
			if (userResult.IsFailure)
			{
				refreshToken = _jwtProvider.GenerateRefreshToken();
				role = "USER";
				_usersRepository.AddUser(new User(email, role, refreshToken));
			}
			else
			{
				refreshToken = userResult.Value.RefreshToken;
				role = userResult.Value.Role;
			}
			var token = _jwtProvider.GenerateToken(email, role);
			return Result.Success(new List<string>() {token, refreshToken});
		}

		public async Task<Result<string>> RefreshToken(string refreshToken, string oldToken)
		{
			var userResult = await _usersRepository.GetUserByRefreshToken(refreshToken);
			if (userResult.IsFailure)
				return Result.Failure<string>(userResult.Error);
			var role = userResult.Value.Role;
			var newToken = _jwtProvider.GenerateToken(userResult.Value.Email, role);
			return Result.Success(newToken);
		}
	}
}
