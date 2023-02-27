using System.Collections.Generic;
using EPiServer.Core;

namespace First3Things.ODPProductAttributeConnector.Service
{
    public interface ICatalogService
    {
        IEnumerable<T> GetEntriesRecursive<T>(ContentReference parentLink) where T : IContent;

        EPiServer.Commerce.Catalog.ContentTypes.CatalogContent GetCatalogRoot();
    }
}
