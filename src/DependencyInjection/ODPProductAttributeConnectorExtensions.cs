using First3Things.ODPProductAttributeConnector.DataPlatform;
using First3Things.ODPProductAttributeConnector.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace First3Things.ODPProductAttributeConnector.DependencyInjection
{
    public static class OdpProductAttributeConnectorExtensions
    {
        public static IServiceCollection AddOdpProductAttributeConnector(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductContentTypeRepository, ProductContentTypeRepository>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<IODPClient, ODPClient>();

            return services;
        }
    }
}
