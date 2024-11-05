using Microsoft.AspNetCore.Mvc;
using OnlineShop.Contracts.Login;
using OnlineShop.Core.Interfaces;


namespace OnlineShop.Controllers
{
	[ApiController]
	[Route("api/v1/auth")]
	public class LoginController : ControllerBase
	{
		private readonly ILoginService _loginService;

		public LoginController(ILoginService loginService)
		{
			_loginService = loginService;
		}

		[Route("login")]
		[HttpPost]
		public async Task<ActionResult> LoginRequest(LoginRequest request)
		{
			_loginService.LoginRequest(request.email);
			return Ok();
		}

		[Route("confirm")]
		[HttpPost]
		public async Task<ActionResult> LoginConfirmRequest(LoginConfirmRequest request)
		{
			var confirmResult = await _loginService.LoginConfirm(request.email, request.otp);
			if (confirmResult.IsFailure)
				return BadRequest(confirmResult.Error);
			HttpContext.Response.Cookies.Append("refresh", confirmResult.Value[1]);
			return Ok(confirmResult.Value[0]);
		}

		[Route("Refresh")]
		[HttpPost]
		public async Task<ActionResult> Refresh(RefreshRequest request)
		{
			var refreshToken = HttpContext.Request.Cookies["refresh"];
			var result = await _loginService.RefreshToken(refreshToken, request.oldToken);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}
	}
}
