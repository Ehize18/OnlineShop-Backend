using CSharpFunctionalExtensions;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces.Repositories
{
	public interface IDeliveryMethodRepository
	{
		Task<Result<int>> Add(DeliveryMethod deliveryMethod);
		Task<Result> Delete(int deliveryMethodId);
		Task<Result<List<DeliveryMethod>>> GetAll();
		Task<Result<DeliveryMethod>> GetById(int id);
		Task<Result> Update(DeliveryMethod deliveryMethod);
	}
}