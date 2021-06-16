using Albelli_TechAssign.Models;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Business.Interfaces
{
    public interface IOrderBusiness
    {
        public Task<OrderModel> GetOrderAsync(int orderId);
        public Task<bool> CreateOrderAsync(OrderModel order);
        public bool IsValidOrder(OrderModel order);
    }
}
