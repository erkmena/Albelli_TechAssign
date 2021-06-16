using Albelli_TechAssign.Business.Interfaces;
using Albelli_TechAssign.Models;
using Albelli_TechAssign.Repository.Interfaces;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly ICardBusiness _cardBusiness;
        private readonly IOrderRepository _orderRepository;
        public OrderBusiness(ICardBusiness cardBusiness, IOrderRepository orderRepository)
        {
            _cardBusiness = cardBusiness;
            _orderRepository = orderRepository;
        }
        public async Task<OrderModel> GetOrderAsync(int orderId)
        {
            return await _orderRepository.GetOrderAsync(orderId);
        }

        public async Task<bool> CreateOrderAsync(OrderModel order)
        {
            if (IsValidOrder(order))
            {
                order.Card = await _cardBusiness.GetCardAsync(order.Card.CardId);
                order.ReqBinWidth = _cardBusiness.CalculateMinBinWidth(order.Card);
                return await _orderRepository.CreateOrderAsync(order);
            }
            return false;
        }

        public bool IsValidOrder(OrderModel order)
        {
            return !string.IsNullOrEmpty(order.CustomerNameSurname) && order.Card.CardId != 0;
        }
    }
}
