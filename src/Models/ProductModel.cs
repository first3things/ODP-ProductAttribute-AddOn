using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace First3Things.ODPProductAttributeConnector.Models
{
    public class ProductModel
    {
        [JsonPropertyName("product_id")]
        public string ProductId { get; set;}

        [JsonExtensionData]
        public IDictionary<string, object> Attributes { get; set; }
    }
}
