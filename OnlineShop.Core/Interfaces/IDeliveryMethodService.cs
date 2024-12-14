using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
	public interface IDeliveryMethodService
	{
		Task<Result<int>> AddMethod(string title, string description);
		Task<Result> DeleteMethod(int id);
		Task<Result<List<DeliveryMethod>>> GetAll();
		Task<Result> UpdateMethod(DeliveryMethod deliveryMethod);
		Task<Result<DeliveryMethod>> GetById(int id);
	}
}