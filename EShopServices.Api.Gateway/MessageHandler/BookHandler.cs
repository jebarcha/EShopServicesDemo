using EShopServices.Api.Gateway.BookRemote;
using EShopServices.Api.Gateway.InterfaceRemote;
using Ocelot.Responses;
using System.Diagnostics;
using System.Text.Json;

namespace EShopServices.Api.Gateway.MessageHandler;

public class BookHandler : DelegatingHandler
{
    private readonly ILogger<BookHandler> _logger;
    private readonly IAuthorRemote _authorRemote;

    public BookHandler(ILogger<BookHandler> logger, IAuthorRemote authorRemote)
    {
        _logger = logger;
        _authorRemote = authorRemote;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var theTime = Stopwatch.StartNew();
        _logger.LogInformation("Start request");

        var response = await base.SendAsync(request, cancellationToken);

        try
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var resultBook = JsonSerializer.Deserialize<BookModelRemote>(content, options);

                var responseAuthor = await _authorRemote.GetAuthor(resultBook!.AuthorBookId ?? Guid.Empty);
                if (responseAuthor.Result)
                {
                    var objAuthor = responseAuthor.Author;
                    resultBook.AuthorData = objAuthor;
                    var resultAsString = JsonSerializer.Serialize(resultBook);
                    response.Content = new StringContent(resultAsString, System.Text.Encoding.UTF8, "application/json");
                }
            }


            _logger.LogInformation($"This process was performed in {theTime.ElapsedMilliseconds}ms");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
        }
        return response;
    }
}
