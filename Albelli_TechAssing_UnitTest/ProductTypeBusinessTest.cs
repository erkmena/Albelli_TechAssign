using Albelli_TechAssign.Business;
using Albelli_TechAssign.Models;
using Albelli_TechAssign.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Albelli_TechAssing_UnitTest
{
    public class ProductTypeBusinessTest
    {
        private Mock<IProductTypeRepository> productTypeRepositoryMock;
        private ProductTypeBusiness productTypeBusiness;
        [SetUp]
        public void Setup()
        {
            productTypeRepositoryMock = new Mock<IProductTypeRepository>();
            productTypeRepositoryMock.Setup(p => p.CreateProductTypeAsync(It.IsAny<ProductTypeModel>())).Returns(Task.FromResult(true));
            productTypeRepositoryMock.Setup(p => p.GetProductTypeAsync(It.IsAny<int>())).Returns(Task.FromResult(MockModels.MockProductTypeModel(1)));

            productTypeBusiness = new ProductTypeBusiness(productTypeRepositoryMock.Object);

        }

        [Test]
        public async Task CreateProductTypeTest()
        {
            Assert.IsTrue(await productTypeBusiness.CreateProductTypeAsync(MockModels.MockProductTypeModel(1)));
            Assert.False(await productTypeBusiness.CreateProductTypeAsync(MockModels.MockProductTypeModel(0)));

            ProductTypeModel productType = MockModels.MockProductTypeModel(1);
            productType.ProductTypeId = 1;
            productType.Width = 0;
            Assert.False(await productTypeBusiness.CreateProductTypeAsync(productType));
        }
        [Test]
        public async Task GetProductTypeTest()
        {
            Assert.IsTrue((await productTypeBusiness.GetProductTypeAsync(1)).ProductTypeId == MockModels.MockProductTypeModel(1).ProductTypeId);
        }

        [Test]
        public async Task IsValidProductTypeTest()
        {
            ProductTypeModel productType = MockModels.MockProductTypeModel(1);
            productType.ProductTypeId = 1;
            productType.Width = 0;
            Assert.False(await productTypeBusiness.CreateProductTypeAsync(productType));
            Assert.False(await productTypeBusiness.CreateProductTypeAsync(MockModels.MockProductTypeModel(0)));
            Assert.IsTrue(await productTypeBusiness.CreateProductTypeAsync(MockModels.MockProductTypeModel(1)));
        }
    }
}