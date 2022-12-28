using EShopServices.Api.Gateway.BookRemote;
using EShopServices.Api.Gateway.InterfaceRemote;
using System.Text.Json;

namespace EShopServices.Api.Gateway.ImplementRemote;

public class AuthorRemote : IAuthorRemote
{
    private readonly IHttpClientFactory _httpClient;
    private readonly ILogger<AuthorRemote> _logger;

    public AuthorRemote(IHttpClientFactory httpClient, ILogger<AuthorRemote> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<(bool Result, AuthorModelRemote Author, string ErrorMessage)> GetAuthor(Guid AuthorId)
    {
        try
        {
            var client = _httpClient.CreateClient("AuthorService");
            var response = await client.GetAsync($"/Author/{AuthorId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<AuthorModelRemote>(content, options);
                return (true, result, null);
            }

            return (false, null, response.ReasonPhrase);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return (false, null, e.Message);
        }
    }
}
