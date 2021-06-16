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
    public class CardBusinessTest
    {
        private Mock<IProductTypeBusiness> productTypeBusinessMock;
        private Mock<ICardRepository> cardRepositoryMock;
        private CardBusiness cardBusiness;
        [SetUp]
        public void Setup()
        {
            productTypeBusinessMock = new Mock<IProductTypeBusiness>();
            productTypeBusinessMock.Setup(c => c.IsValidProductType(It.IsAny<ProductTypeModel>())).Returns(true);

            cardRepositoryMock = new Mock<ICardRepository>();
            cardRepositoryMock.Setup(c => c.CreateCardAsync(It.IsAny<CardModel>())).Returns(Task.FromResult(true));
            cardRepositoryMock.Setup(c => c.GetCardAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockCardModel(1,1)));
            cardBusiness = new CardBusiness(productTypeBusinessMock.Object, cardRepositoryMock.Object);
        }

        [Test]
        public async Task GetCardTests()
        {
            CardModel card = await cardBusiness.GetCardAsync(1);
            CardModel mockedCard = MockModels.MockCardModel(1, 1);
            Assert.IsTrue(mockedCard.CardId == card.CardId);
        }

        [Test]
        [TestCase(1,2)]
        [TestCase(5, 10)]
        [TestCase(3, 7)]
        public async Task CreateCardTest(int quantity, int quantity2)
        {
            CardModel cardModel = MockModels.MockCardModel(quantity, quantity2);
            Assert.IsTrue(await cardBusiness.CreateCardAsync(cardModel));
        }
        [Test]
        [TestCase(0,4)]
        public async Task CreateCardTestFalse(int quantity, int quantity2)
        {
            CardModel cardModel = MockModels.MockCardModel(quantity, quantity2);
            Assert.IsFalse(await cardBusiness.CreateCardAsync(cardModel));
        }
        [Test]
        [TestCase(1, 2)]
        [TestCase(5, 10)]
        [TestCase(3, 7)]
        public void CalculateBinWidthTest(int quantity, int quantity2)
        {
            decimal returnValue = cardBusiness.CalculateMinBinWidth(MockModels.MockCardModel(quantity, quantity2));
            decimal checkValue = (quantity * 5) + (Math.Ceiling(Convert.ToDecimal(quantity2) / 4) * 5);
            Assert.IsTrue(returnValue == checkValue);
        }
    }
}