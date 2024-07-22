using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class SmartCardService
{
    private readonly HttpClient _httpClient;

    public SmartCardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetAtrAsync()
    {
        try
        {
            // Cambia a la dirección local del cliente
            var response = await _httpClient.GetAsync("http://localhost:5000/");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                // Deserializa el JSON para extraer el ATR
                var atrResponse = JsonSerializer.Deserialize<AtrResponse>(result);
                return atrResponse.atr;
            }
            else
            {
                return $"Error: {response.ReasonPhrase}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}

public class AtrResponse
{
    public string? atr { get; set; }
}
