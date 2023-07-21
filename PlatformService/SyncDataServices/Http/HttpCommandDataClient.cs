using PlatformService.Dto;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var httpContent = new StringContent(JsonSerializer.Serialize(platformReadDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}/api/platforms", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("----> sync post to command service was OK!");
                return;
            }

            Console.WriteLine("----> sync post to command service was NOT OK!");
        }
    }
}
