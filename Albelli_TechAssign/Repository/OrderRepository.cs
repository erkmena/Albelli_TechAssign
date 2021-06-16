using Albelli_TechAssign.Models;
using Albelli_TechAssign.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ICardRepository _cardRepository;
        private readonly string _orderInsertQuery = "INSERT INTO [Order](CardId,ReqBinWidth,CustomerNameSurname) VALUES (@CardId, @ReqBinWidth,@CustomerNameSurname)";
        private readonly string _orderSelectQuery = "SELECT OrderId, ReqBinWidth,CustomerNameSurname,CardId FROM [ORDER] WHERE OrderId = @OrderId";
        private IConfiguration _configuration;
        public OrderRepository(ICardRepository cardRepository, IConfiguration configuration)
        {
            _cardRepository = cardRepository;
            _configuration = configuration;
        }
        public async Task<bool> CreateOrderAsync(OrderModel order)
        {
            bool result = false;
            using (var connection = new SqlConnection(new SqlConfiguration(_configuration).GetConnectionString()))
            {
                var effectedRow = await connection.ExecuteAsync(_orderInsertQuery, new { CardId = order.Card.CardId, ReqBinWidth = order.ReqBinWidth, CustomerNameSurname = order.CustomerNameSurname });
                result = (effectedRow == 1) ? true : false;
            }
            return result;
        }

        public async Task<OrderModel> GetOrderAsync(int orderId)
        {
            OrderModel resultOrder = new OrderModel();
            using (var connection = new SqlConnection(new SqlConfiguration(_configuration).GetConnectionString()))
            {
                var resultOrderList = await connection.QueryAsync<OrderModel, CardModel, OrderModel>(_orderSelectQuery, (o, c) =>
                {
                    OrderModel order = new OrderModel();
                    order = o;
                    order.Card = c;
                    return order;
                }, new { OrderId = orderId }, splitOn: "OrderId, CardId");
                if (resultOrderList.AsList().Count > 0)
                {
                    resultOrder = resultOrderList.AsList()[0];
                    resultOrder.Card = await _cardRepository.GetCardAsync(resultOrder.Card.CardId);
                }
                else
                {
                    throw new Exception("Order Can Not Be Find");
                }
            }
            return resultOrder;
        }
    }
}
