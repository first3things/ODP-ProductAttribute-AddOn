using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;

namespace First3Things.ODPProductAttributeConnector.Service
{
    public class CatalogService : ICatalogService
    {
        private readonly IContentLoader _contentLoader;

        public CatalogService() : base()
        {
            _contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        }

        public CatalogService(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public IEnumerable<T> GetEntriesRecursive<T>(ContentReference parentLink) where T : IContent
        {
            var defaultCulture = ContentLanguage.PreferredCulture;

            foreach (var nodeContent in LoadChildrenBatched<NodeContent>(parentLink, defaultCulture))
            {
                foreach (var entry in GetEntriesRecursive<T>(nodeContent.ContentLink))
                {
                    yield return entry;
                }
            }

            foreach (var entry in LoadChildrenBatched<T>(parentLink, defaultCulture))
            {
                yield return entry;
            }
        }

        public EPiServer.Commerce.Catalog.ContentTypes.CatalogContent GetCatalogRoot()
        {
            var referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();
            var repository = ServiceLocator.Current.GetInstance<IContentRepository>();
            return repository.GetChildren<EPiServer.Commerce.Catalog.ContentTypes.CatalogContent>(referenceConverter.GetRootLink()).FirstOrDefault();
        }

        private IEnumerable<T> LoadChildrenBatched<T>(ContentReference parentLink, CultureInfo defaultCulture) where T : IContent
        {
            var start = 0;

            while (true)
            {
                var batch = _contentLoader.GetChildren<T>(parentLink, defaultCulture, start, 50);
                if (!batch.Any())
                {
                    yield break;
                }

                foreach (var content in batch)
                {
                    if (!parentLink.CompareToIgnoreWorkID(content.ParentLink))
                    {
                        continue;
                    }

                    yield return content;
                }

                start += 50;
            }
        }
    }
}