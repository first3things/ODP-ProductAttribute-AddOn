using EPiServer.Core;

namespace ODPProductAttributeConnector.Service
{
    public interface ICatalogService
    {
        IEnumerable<T> GetEntriesRecursive<T>(ContentReference parentLink) where T : IContent;

        EPiServer.Commerce.Catalog.ContentTypes.CatalogContent GetCatalogRoot();
    }
}
