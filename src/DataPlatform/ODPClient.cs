using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using First3Things.ODPProductAttributeConnector.Helpers;
using First3Things.ODPProductAttributeConnector.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace First3Things.ODPProductAttributeConnector.DataPlatform
{

    public class ODPClient : IODPClient
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ODPClient> _logger;

        public ODPClient(IConfiguration configuration, ILogger<ODPClient> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public bool SendProductAttributesToDataPlatform(List<ProductModel> products)
        {
            bool success = true;

            HttpClient client = new();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("x-api-key", _configuration.GetValue<string>("ODPConnector:apiKey"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string apiUrl = $"https://{_configuration.GetValue<string>("ODPConnector:apiHost")}/v3/objects/products";

            foreach (var productChunk in products.Chunk(500))
            {
                var json = JsonSerializer.Serialize(productChunk);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = Task.Run(() => client.PostAsync(apiUrl, data));

                var result = response.Result;

                if (!result.IsSuccessStatusCode)
                {
                    _logger.LogError($"OdpProductCatalogScheduledJob -> API Call failed: {result.StatusCode} / {result.ReasonPhrase}");
                    success = false;
                }
            }

            return success;
        }
    }
}