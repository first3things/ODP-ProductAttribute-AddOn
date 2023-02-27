using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EPiServer.DataAbstraction;
using First3Things.ODPProductAttributeConnector.DataAnnotations;

namespace First3Things.ODPProductAttributeConnector.Service
{
    public class ProductContentTypeRepository : IProductContentTypeRepository
    {
        private readonly IContentTypeRepository _contentTypeRepository;

        public ProductContentTypeRepository(IContentTypeRepository contentTypeRepository)
        {
            _contentTypeRepository = contentTypeRepository;
        }

        public List<ContentType> GetProductContentTypes()
        {
            var contentTypes = _contentTypeRepository.List()
                .Where(o => o.Name != "SysRoot" && o.Name != "SysRecycleBin" && IsValidType("Entry", o.ModelType))
                .OrderBy(o => o.Name);

            return contentTypes.ToList();
        }

        public List<PropertyInfo> GetContentTypeProperties(ContentType contentType)
        {
            var properties = contentType.ModelType.GetProperties().Where(x => Attribute.IsDefined(x, typeof(OdpProductSyncAttribute)));

            List<PropertyInfo> propertyInfos = null;

            propertyInfos = properties.ToList();

            return propertyInfos;
        }

        private bool IsValidType(string type, Type inputType)
        {
            if (string.IsNullOrEmpty(type))
            {
                return false;
            }

            var contentType = _contentTypes[type];

            return contentType.IsAssignableFrom(inputType);
        }

        private readonly Dictionary<string, Type> _contentTypes = new Dictionary<string, Type>()
        {
            { "Entry", Type.GetType("EPiServer.Commerce.Catalog.ContentTypes.EntryContentBase, EPiServer.Business.Commerce", false) }
        };
    }
}