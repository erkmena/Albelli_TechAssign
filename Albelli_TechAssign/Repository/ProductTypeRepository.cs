using Albelli_TechAssign.Models;
using Albelli_TechAssign.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Albelli_TechAssign.Repository
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly string _productTypeInsertQuery = "INSERT INTO PRODUCTTYPE (Width, MaxStockSize, ProductTypeName) VALUES (@Width, @MaxStockSize, @ProductTypeName)";
        private readonly string _productTypeSelectQuery = "SELECT * FROM PRODUCTTYPE Where ProductTypeId = @ProductTypeId";
        private IConfiguration _configuration;

        public ProductTypeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CreateProductTypeAsync(ProductTypeModel productType)
        {
            bool result = false;    
            using (var connection = new SqlConnection(new SqlConfiguration(_configuration).GetConnectionString()))
            {
                var effectedRow = await connection.ExecuteAsync(_productTypeInsertQuery,productType);
                result = (effectedRow == 1) ? true : false;
            }
            return result;
        }

        public  async Task<ProductTypeModel> GetProductTypeAsync(int productTypeId)
        {
            using (var connection = new SqlConnection(new SqlConfiguration(_configuration).GetConnectionString()))
            {
                var productType = await connection.QueryAsync<ProductTypeModel>(_productTypeSelectQuery, new { ProductTypeId= productTypeId });
                if(productType.AsList().Count > 0)
                {
                    return productType.AsList()[0];
                }
                else
                {
                    throw new Exception("ProductType Can Not Be Find");
                }
            }
        }
    }
}
