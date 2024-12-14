using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Contracts.Basket;
using OnlineShop.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Controllers
{
	[ApiController]
	[Route("api/v1/basket")]
	public class BasketController : ControllerBase
	{
		private readonly IBasketsService _basketsService;
		public BasketController(IBasketsService basketsService)
		{
			_basketsService = basketsService;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<BasketResponse>> GetBasket()
		{
			var email = HttpContext.User.Claims.ToList()[1].Value;
			var basketResult = await _basketsService.GetCurrentBasket(email);
			if (basketResult.IsFailure)
				return BadRequest(basketResult.Error);
			var basketItems = new List<BasketItemResponse>();
			foreach (var basketItem in basketResult.Value.Items)
				basketItems.Add(new(
					basketItem.GoodId,
					basketItem.Good.Name, basketItem.Good.Description,
					basketItem.Good.Price, basketItem.Count));
			var response = new BasketResponse(basketItems.Count, basketItems.Sum(x => x.price * x.count), basketItems);
			return Ok(response);
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult> AddItem(int goodId)
		{
			var email = HttpContext.User.Claims.ToList()[1].Value;
			var result = await _basketsService.AddItemToBasket(goodId, email);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok();
		}

		[HttpPatch]
		[Authorize]
		public async Task<ActionResult> ChangeItemCount(int basketItemId, int count)
		{
			var email = HttpContext.User.Claims.ToList()[1].Value;
			var result = await _basketsService.ChangeItemCountInBasket(email, basketItemId, count);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok();
		}

		[HttpDelete]
		[Authorize]
		public async Task<ActionResult> DeleteItem(int basketItemId)
		{
			var email = HttpContext.User.Claims.ToList()[1].Value;
			var result = await _basketsService.DeleteItemFromBasket(email, basketItemId);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return Ok();
		}
	}
}
