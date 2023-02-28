using System.Collections.Generic;
using First3Things.ODPProductAttributeConnector.Models;

namespace First3Things.ODPProductAttributeConnector.DataPlatform
{

    public interface IODPClient
    {
        bool SendProductAttributesToDataPlatform(List<ProductModel> products);
    }
}