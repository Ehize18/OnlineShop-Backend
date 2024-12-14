using CSharpFunctionalExtensions;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.Application.Services
{
	public class DeliveryMethodService : IDeliveryMethodService
	{
		private readonly IDeliveryMethodRepository _deliveryMethodRepository;

		public DeliveryMethodService(IDeliveryMethodRepository deliveryMethodRepository)
		{
			_deliveryMethodRepository = deliveryMethodRepository;
		}

		public async Task<Result<int>> AddMethod(string title, string description)
		{
			return await _deliveryMethodRepository.Add(new(title, description));
		}

		public async Task<Result<List<DeliveryMethod>>> GetAll()
		{
			return await _deliveryMethodRepository.GetAll();
		}

		public async Task<Result> UpdateMethod(DeliveryMethod deliveryMethod)
		{
			return await _deliveryMethodRepository.Update(deliveryMethod);
		}

		public async Task<Result> DeleteMethod(int id)
		{
			return await _deliveryMethodRepository.Delete(id);
		}

		public async Task<Result<DeliveryMethod>> GetById(int id)
		{
			return await _deliveryMethodRepository.GetById(id);
		}
	}
}
