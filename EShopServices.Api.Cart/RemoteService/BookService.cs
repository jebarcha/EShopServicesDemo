using EShopServices.Api.Cart.RemoteInterface;
using EShopServices.Api.Cart.RemoteModel;
using System.Text.Json;

namespace EShopServices.Api.Cart.RemoteService;

public class BookService : IBookService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly ILogger _logger;

    public BookService(IHttpClientFactory httpClient) //, ILogger logger)
    {
        _httpClient = httpClient;
        //_logger = logger;
    }

    public async Task<(bool result, RemoteBook? book, string ErrorMessage)> GetBook(Guid bookId)
    {
        try
        {
            var client = _httpClient.CreateClient("Books");
            var response = await client.GetAsync($"api/Book/{bookId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<RemoteBook>(content, options);
                return (true, result, "");
            }

            return (false, null, response.ReasonPhrase!);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}
