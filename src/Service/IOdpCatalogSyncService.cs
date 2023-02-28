using System.Collections.Generic;
using System.Reflection;
using EPiServer.Commerce.Catalog.ContentTypes;

namespace First3Things.ODPProductAttributeConnector.Service
{

    public interface IOdpCatalogSyncService
    {
        bool ProcessProductContentType<T>(IEnumerable<PropertyInfo> propertyInfos) where T : CatalogContentBase;
    }
}