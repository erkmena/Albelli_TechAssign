using Albelli_TechAssign.Models;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Repository.Interfaces
{
    public interface IOrderRepository
    {
        public Task<bool> CreateOrderAsync(OrderModel order);
        public Task<OrderModel> GetOrderAsync(int orderId);
    }
}
