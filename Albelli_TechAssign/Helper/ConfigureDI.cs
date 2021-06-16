using Albelli_TechAssign.Business;
using Albelli_TechAssign.Business.Interfaces;
using Albelli_TechAssign.Repository;
using Albelli_TechAssign.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Albelli_TechAssign.Helper
{
    public class ConfigureDI
    {
        internal static void ConfigureDependencyInjections(IServiceCollection services)
        {
            services.AddSingleton<ICardBusiness, CardBusiness>();
            services.AddSingleton<IOrderBusiness, OrderBusiness>();
            services.AddSingleton<IProductTypeBusiness, ProductTypeBusiness>();

            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductTypeRepository, ProductTypeRepository>();

        }
    }
}
