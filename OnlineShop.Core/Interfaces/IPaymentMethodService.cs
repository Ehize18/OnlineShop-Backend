using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
	public interface IPaymentMethodService
	{
		Task<Result<int>> AddMethod(string title, string description);
		Task<Result> DeleteMethod(int id);
		Task<Result<List<PaymentMethod>>> GetAll();
		Task<Result<PaymentMethod>> GetById(int id);
		Task<Result> UpdateMethod(PaymentMethod paymentMethod);
	}
}