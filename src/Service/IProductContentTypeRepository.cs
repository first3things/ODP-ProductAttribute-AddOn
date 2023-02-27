using System.Collections.Generic;
using System.Reflection;
using EPiServer.DataAbstraction;

namespace First3Things.ODPProductAttributeConnector.Service
{

    public interface IProductContentTypeRepository
    {
        List<ContentType> GetProductContentTypes();

        List<PropertyInfo> GetContentTypeProperties(ContentType contentType);
    }
}