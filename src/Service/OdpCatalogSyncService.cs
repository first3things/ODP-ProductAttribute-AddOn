using System.Collections.Generic;
using System.Reflection;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.ServiceLocation;
using First3Things.ODPProductAttributeConnector.DataPlatform;
using First3Things.ODPProductAttributeConnector.Models;
using Mediachase.Commerce.Catalog;

namespace First3Things.ODPProductAttributeConnector.Service
{
    public class OdpCatalogSyncService : IOdpCatalogSyncService
    {
        private readonly ICatalogService _catalogService;
        private readonly ReferenceConverter _referenceConverter;
        private readonly IODPClient _client;

        public OdpCatalogSyncService()
        {
            _catalogService = ServiceLocator.Current.GetInstance<ICatalogService>();
            _referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();
            _client = ServiceLocator.Current.GetInstance<IODPClient>();
        }

        public void ProcessProductContentType<T>(IEnumerable<PropertyInfo> propertyInfos) where T : CatalogContentBase
        {
            var catalogRoot = _catalogService.GetCatalogRoot();

            var entries = _catalogService.GetEntriesRecursive<T>(catalogRoot.ContentLink);

            List<ProductModel> productModels = new List<ProductModel>();

            // build JSON Model
            foreach (var entry in entries)
            {
                var attributes = new Dictionary<string, object>();

                var code = _referenceConverter.GetCode(entry.ContentLink);

                foreach (var propertyInfo in propertyInfos)
                {
                    var value = propertyInfo.GetValue(entry);

                    if (value != null)
                        attributes.Add(propertyInfo.Name, value.ToString());
                }

                productModels.Add(new ProductModel()
                {
                    ProductId = code,
                    Attributes = attributes
                });
            }

            if (productModels.Count > 0)
            {
                _client.SendProductAttributesToDataPlatform(productModels);
            }
        }
    }
}