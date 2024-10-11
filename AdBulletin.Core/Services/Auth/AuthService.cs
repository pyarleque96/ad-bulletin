using AdBulletin.Infrastructure.ExceptionHandler;
using System.Text.Json.Nodes;

namespace AdBulletin.Core.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GetJwtTokenFromApi(string email, string password)
    {
        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync($"{_configuration["Hosts:WebAPI:Uri"]}/{_configuration["Hosts:WebAPI:EndPoints:AuthToken"]}", new { Username = email, Password = password });

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                    return result.token;
                }
                else
                {
                    // Read content
                    var errorContent = await response.Content.ReadAsStringAsync();

                    // Parse the JSON content into a JsonNode
                    JsonNode? jsonNode = JsonNode.Parse(errorContent);

                    // Navigate to the errors property and convert it to a string
                    string? errorsJson = jsonNode?["errors"]?.ToJsonString();

                    //throw new DomainException($"GetJwtTokenFromApi() - errors: {errorsJson} .");
                }

                return string.Empty;
            }
        }
        catch (DomainException ex)
        {
            throw new DomainException($"GetJwtTokenFromApi() - errors: {ex.Message} .");
        }
    }

    private class TokenResponse
    {
        public string token { get; set; }
    }
}
