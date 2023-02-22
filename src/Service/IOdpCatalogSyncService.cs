using System.Reflection;
using EPiServer.Commerce.Catalog.ContentTypes;

namespace ODPProductAttributeConnector.Service;

public interface IOdpCatalogSyncService
{
    void ProcessProductContentType<T>(IEnumerable<PropertyInfo> propertyInfos) where T : CatalogContentBase;
}