using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Contracts.DeliveryMethods;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Controllers
{
	[ApiController]
	[Route("api/v1/payment-methods")]
	public class PaymentMethodController : ControllerBase
	{
		private readonly IPaymentMethodService _paymentMethodService;
		public PaymentMethodController(IPaymentMethodService paymentMethodService)
		{
			_paymentMethodService = paymentMethodService;
		}

		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> Add(MethodRequest request)
		{
			var result = await _paymentMethodService.AddMethod(request.title, request.title);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok(result.Value);
		}

		[HttpGet]
		public async Task<ActionResult<List<MethodResponse>>> GetAll()
		{
			var result = await _paymentMethodService.GetAll();
			if (result.IsFailure)
				return BadRequest(result.Error);
			var methods = result.Value;
			var response = methods.Select(x => new MethodResponse((int)x.Id, x.Title, x.Description));
			return Ok(response);
		}

		[HttpPatch("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> UpdateMethod(int id, PatchDeliveryMethod patch)
		{
			var methodResult = await _paymentMethodService.GetById(id);
			if (methodResult.IsFailure)
				return BadRequest(methodResult.Error);
			var method = methodResult.Value;
			var newMethod = new PaymentMethod(
				id,
				patch.IsFieldPresent(nameof(method.Title)) ? patch.Title : method.Title,
				patch.IsFieldPresent(nameof(method.Description)) ? patch.Description : method.Description
				);
			var patchResult = await _paymentMethodService.UpdateMethod(newMethod);
			if (patchResult.IsFailure)
				return BadRequest(patchResult.Error);
			return Ok();
		}

		[HttpDelete("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<ActionResult> DeleteMethod(int id)
		{
			var result = await _paymentMethodService.DeleteMethod(id);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok();
		}
	}
}
