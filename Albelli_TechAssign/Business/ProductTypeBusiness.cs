using Albelli_TechAssign.Business.Interfaces;
using Albelli_TechAssign.Models;
using Albelli_TechAssign.Repository.Interfaces;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Business
{
    public class ProductTypeBusiness : IProductTypeBusiness
    {
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeBusiness(IProductTypeRepository productTypeBusiness)
        {
            _productTypeRepository = productTypeBusiness;
        }
        public async Task<ProductTypeModel> GetProductTypeAsync(int productTypeId)
        {
            return await _productTypeRepository.GetProductTypeAsync(productTypeId);
        }

        public async Task<bool> CreateProductTypeAsync(ProductTypeModel productType)
        {
            if (IsValidProductType(productType))
            {
                return await _productTypeRepository.CreateProductTypeAsync(productType);
            }
            return false;
        }

        public bool IsValidProductType(ProductTypeModel productType)
        {
            return productType.MaxStockSize != 0 && productType.Width != 0;
        }
    }
}
