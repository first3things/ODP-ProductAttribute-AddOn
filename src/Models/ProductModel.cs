using System.Text.Json.Serialization;

namespace ODPProductAttributeConnector.Models
{
    public class ProductModel
    {
        [JsonPropertyName("product_id")]
        public string ProductId { get; set;}

        [JsonExtensionData]
        public IDictionary<string, object> Attributes { get; set; }
    }
}
