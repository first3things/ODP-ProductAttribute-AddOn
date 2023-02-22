using System.Reflection;
using EPiServer.DataAbstraction;

namespace ODPProductAttributeConnector.Service;

public interface IProductContentTypeRepository
{
    List<ContentType> GetProductContentTypes();

    List<PropertyInfo> GetContentTypeProperties(ContentType contentType);
}