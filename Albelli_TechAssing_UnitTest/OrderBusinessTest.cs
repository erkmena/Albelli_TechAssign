using Albelli_TechAssign.Business;
using Albelli_TechAssign.Business.Interfaces;
using Albelli_TechAssign.Models;
using Albelli_TechAssign.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Albelli_TechAssing_UnitTest
{
    public class OrderBusinessTest
    {
        private Mock<ICardBusiness> cardBusinessMock;
        private Mock<IOrderRepository> orderRepositoryMock;
        private OrderBusiness orderBusiness;
        [SetUp]
        public void Setup()
        {
            cardBusinessMock = new Mock<ICardBusiness>();
            cardBusinessMock.Setup(c => c.CreateCardAsync(It.IsAny<CardModel>())).Returns(Task.FromResult(true));
            cardBusinessMock.Setup(c => c.CalculateMinBinWidth(It.IsAny<CardModel>())).Returns(Convert.ToDecimal(20.2));
            cardBusinessMock.Setup(c => c.IsValidCard(It.IsAny<CardModel>())).Returns(true);

            orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(o => o.CreateOrderAsync(It.IsAny<OrderModel>())).Returns(Task.FromResult(true));
            orderRepositoryMock.Setup(o => o.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockOrderModel()));

            orderBusiness = new OrderBusiness(cardBusinessMock.Object, orderRepositoryMock.Object);
        }

        [Test]
        public async Task CreateOrderTest()
        {
            Assert.IsTrue(await orderBusiness.CreateOrderAsync(MockModels.MockOrderModel()));
        }
        [Test]
        public async Task GetOrderTest()
        {
            Assert.IsTrue((await orderBusiness.GetOrderAsync(1)).OrderId == MockModels.MockOrderModel().OrderId);
        }
        [Test]
        public void IsValidOrder()
        {
            Assert.IsTrue(orderBusiness.IsValidOrder(MockModels.MockOrderModel()));
        }
        [Test]
        public void IsValidOrderFalse()
        {
            OrderModel order = MockModels.MockOrderModel();
            order.CustomerNameSurname = string.Empty;
            Assert.IsFalse(orderBusiness.IsValidOrder(order));
        }
    }
}