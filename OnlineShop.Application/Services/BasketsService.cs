using CSharpFunctionalExtensions;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.Application.Services
{
	public class BasketsService : IBasketsService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IBasketItemRepository _basketItemRepository;
		private readonly IUsersRepository _usersRepository;
		private readonly IGoodsRepository _goodsRepository;

		public BasketsService(IBasketRepository basketRepository, IBasketItemRepository basketItemRepository,
			IUsersRepository usersRepository, IGoodsRepository goodsRepository)
		{
			_basketRepository = basketRepository;
			_basketItemRepository = basketItemRepository;
			_usersRepository = usersRepository;
			_goodsRepository = goodsRepository;
		}

		public async Task<Result> AddItemToBasket(int goodId, string userEmail)
		{
			var userResult = await _usersRepository.GetUserByEmail(userEmail);
			if (userResult.IsFailure)
				return Result.Failure($"User with email: '{userEmail}' not found");
			var goodResult = await _goodsRepository.GetById(goodId);
			if (goodResult.IsFailure)
				return Result.Failure($"Good with id: '{goodId}' not found");
			var user = userResult.Value;
			var currentBasketResult = await _basketRepository.GetCurrent((int)user.Id);
			Basket currentBasket;
			if (currentBasketResult.IsFailure)
			{
				var basketCreationResult = await _basketRepository.Add(new((int)user.Id));
				if (basketCreationResult.IsFailure)
					return Result.Failure($"Failed to create new basket");
				currentBasket = basketCreationResult.Value;
			}
			else
			{
				currentBasket = currentBasketResult.Value;
			}
			var basketItemResult = await _basketItemRepository.Add(new((int)currentBasket.Id, goodId));
			if (basketItemResult.IsFailure)
				return Result.Failure($"Failed to add item to basket");
			return Result.Success();
		}

		public async Task<Result<Basket>> GetCurrentBasket(string userEmail)
		{
			var userResult = await _usersRepository.GetUserByEmail(userEmail);
			if (userResult.IsFailure)
				return Result.Failure<Basket>($"User with email: '{userEmail}' not found");
			var user = userResult.Value;
			var currentBasketResult = await _basketRepository.GetCurrent((int)user.Id);
			Basket currentBasket;
			if (currentBasketResult.IsFailure)
			{
				var basketCreationResult = await _basketRepository.Add(new((int)user.Id));
				if (basketCreationResult.IsFailure)
					return Result.Failure<Basket>($"Failed to create new basket");
				currentBasket = basketCreationResult.Value;
			}
			else
			{
				currentBasket = currentBasketResult.Value;
			}
			return Result.Success(currentBasket);
		}

		public async Task<Result> ChangeItemCountInBasket(string userEmail, int goodId, int count)
		{
			var basketResult = await GetCurrentBasket(userEmail);
			if (basketResult.IsFailure)
				return Result.Failure(basketResult.Error);
			var basketItems = basketResult.Value.Items;
			if (!basketItems.Any(x => x.GoodId == goodId))
				return Result.Failure("Item in basket not found");
			var item = basketItems.First(x => x.GoodId == goodId);
			var updateResult = await _basketItemRepository.UpdateItem((int)item.Id, count);
			return updateResult;
		}

		public async Task<Result> DeleteItemFromBasket(string userEmail, int goodId)
		{
			var basketResult = await GetCurrentBasket(userEmail);
			if (basketResult.IsFailure)
				return Result.Failure(basketResult.Error);
			var basketItems = basketResult.Value.Items;
			if (!basketItems.Any(x => x.Id == goodId))
				return Result.Failure("Item in basket not found");
			var item = basketItems.First(x => x.GoodId == goodId);
			var deleteResult = await _basketItemRepository.DeleteItem((int)item.Id);
			return deleteResult;
		}
	}
}
