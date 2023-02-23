using First3Things.ODPProductAttributeConnector.Models;

namespace First3Things.ODPProductAttributeConnector.DataPlatform;

public interface IODPClient
{
    void SendProductAttributesToDataPlatform(List<ProductModel> products);
}