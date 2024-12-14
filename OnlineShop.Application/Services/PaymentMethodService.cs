using CSharpFunctionalExtensions;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Interfaces.Repositories;
using OnlineShop.Core.Models;

namespace OnlineShop.Application.Services
{
	public class PaymentMethodService : IPaymentMethodService
	{
		private readonly IPaymentMethodRepository _paymentMethodRepository;

		public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository)
		{
			_paymentMethodRepository = paymentMethodRepository;
		}

		public async Task<Result<int>> AddMethod(string title, string description)
		{
			return await _paymentMethodRepository.Add(new(title, description));
		}

		public async Task<Result<List<PaymentMethod>>> GetAll()
		{
			return await _paymentMethodRepository.GetAll();
		}

		public async Task<Result> UpdateMethod(PaymentMethod paymentMethod)
		{
			return await _paymentMethodRepository.Update(paymentMethod);
		}

		public async Task<Result> DeleteMethod(int id)
		{
			return await _paymentMethodRepository.Delete(id);
		}

		public async Task<Result<PaymentMethod>> GetById(int id)
		{
			return await _paymentMethodRepository.GetById(id);
		}
	}
}
