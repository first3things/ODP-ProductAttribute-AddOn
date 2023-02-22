using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ODPProductAttributeConnector.Models;

namespace ODPProductAttributeConnector.DataPlatform;

public class ODPClient : IODPClient
{
    private readonly IConfiguration _configuration;

    public ODPClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendProductAttributesToDataPlatform(List<ProductModel> products)
    {
        HttpClient client = new();

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Add("x-api-key", _configuration.GetValue<string>("ODPConnector:apiKey"));
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var json = JsonSerializer.Serialize(products);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        string apiUrl = $"https://{_configuration.GetValue<string>("ODPConnector:apiHost")}/v3/objects/products";

        var response = Task.Run(() => client.PostAsync(apiUrl, data));

        var result = response.Result;
    }
}