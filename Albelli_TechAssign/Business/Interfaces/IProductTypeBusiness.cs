using Albelli_TechAssign.Models;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Business.Interfaces
{
    public interface IProductTypeBusiness
    {
        public Task<ProductTypeModel> GetProductTypeAsync(int ProductTypeId);
        public Task<bool> CreateProductTypeAsync(ProductTypeModel ProductType);
        public bool IsValidProductType(ProductTypeModel card);
    }
}
