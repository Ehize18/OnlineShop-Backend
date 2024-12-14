using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces.Repositories
{
	public interface IPaymentMethodRepository
	{
		Task<Result<int>> Add(PaymentMethod paymentMethod);
		Task<Result> Delete(int paymentMethodId);
		Task<Result<List<PaymentMethod>>> GetAll();
		Task<Result<PaymentMethod>> GetById(int id);
		Task<Result> Update(PaymentMethod paymentMethod);
	}
}