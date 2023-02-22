using ODPProductAttributeConnector.Models;

namespace ODPProductAttributeConnector.DataPlatform;

public interface IODPClient
{
    void SendProductAttributesToDataPlatform(List<ProductModel> products);
}