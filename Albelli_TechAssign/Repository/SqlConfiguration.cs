using Microsoft.Extensions.Configuration;

namespace Albelli_TechAssign.Repository
{
    public class SqlConfiguration
    {
        private IConfiguration _configuration;
        public SqlConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetConnectionString(bool isMaster = false)
        {
            string connectionString;
            if(_configuration.GetSection("DbChoice").Value.ToLower() == "azure")
            {
                return _configuration.GetSection("ConnectionStrings").GetSection("AzureConnection").Value;
            }
            if (isMaster)
            {
                connectionString = _configuration.GetSection("ConnectionStrings").GetSection("MasterConnection").Value;
            }
            else
            {
                connectionString = _configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            }
            return connectionString;
        }
    }
}
