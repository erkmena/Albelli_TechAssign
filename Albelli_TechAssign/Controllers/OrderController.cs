using Albelli_TechAssign.Business.Interfaces;
using Albelli_TechAssign.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        public readonly IOrderBusiness _orderBusiness;
        public OrderController(ILogger<OrderController> logger, IOrderBusiness orderBusiness)
        {
            _logger = logger;
            _orderBusiness = orderBusiness;
        }
        [HttpGet]
        public async Task<OrderModel> GetOrderAsync(int orderId)
        {
            return await _orderBusiness.GetOrderAsync(orderId);
        }
        [HttpPost]
        public async Task<bool> CreateOrderAsync(OrderModel order)
        {
            return await _orderBusiness.CreateOrderAsync(order);
        }
    }
}
