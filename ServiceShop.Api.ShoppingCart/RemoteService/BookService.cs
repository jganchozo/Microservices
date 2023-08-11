
using MySqlX.XDevAPI.Common;
using ServiceShop.Api.ShoppingCart.RemoteInterface;
using ServiceShop.Api.ShoppingCart.RemoteModel;
using System.Text.Json;

namespace ServiceShop.Api.ShoppingCart.RemoteService
{
    public class BookService : IBookService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<BookService> _logger;

        public BookService(IHttpClientFactory httpClient, ILogger<BookService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool result, RemoteBook? book, string? errorMessage)> GetRemoteBook(Guid bookId)
        {
            try
            {
                var client = _httpClient.CreateClient("Books");
                var response = await client.GetAsync($"/api/book/{bookId}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<RemoteBook>(content, options);

                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
