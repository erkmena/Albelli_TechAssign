using Albelli_TechAssign.Business.Interfaces;
using Albelli_TechAssign.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly ILogger<ProductTypeController> _logger;
        private readonly IProductTypeBusiness _productTypeBusiness;
        public ProductTypeController(ILogger<ProductTypeController> logger, IProductTypeBusiness productTypeBusiness)
        {
            _logger = logger;
            _productTypeBusiness = productTypeBusiness;
        }
        [HttpGet]
        public async Task<ProductTypeModel> GetProductType(int productTypeId)
        {
            return await _productTypeBusiness.GetProductTypeAsync(productTypeId);
        }
        [HttpPost]
        public async Task<bool> CreateProductType(ProductTypeModel productType)
        {
            return await _productTypeBusiness.CreateProductTypeAsync(productType);
        }
    }
}
