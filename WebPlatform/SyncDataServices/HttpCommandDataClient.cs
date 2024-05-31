using System.Text;
using System.Text.Json;

namespace WebPlatform.SyncDataServices;

public class HttpCommandDataClient : ICommandServices
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    
    public HttpCommandDataClient(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }
    
    public async Task SendPlatformToComment(PlatformReadDto platformReadDto)
    {
        var httpContent = new StringContent(JsonSerializer.Serialize(platformReadDto), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{_configuration["CommandService"]}", httpContent);

        Console.WriteLine(response.IsSuccessStatusCode
            ? "Sync Post To CommandService is OK!"
            : "Sync Post To CommandService is FAILED");
    }
}