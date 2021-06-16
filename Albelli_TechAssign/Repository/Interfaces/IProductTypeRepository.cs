using Albelli_TechAssign.Models;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Repository.Interfaces
{
    public interface IProductTypeRepository
    {
        public Task<bool> CreateProductTypeAsync(ProductTypeModel productType);
        public Task<ProductTypeModel> GetProductTypeAsync(int productTypeId);
    }
}
