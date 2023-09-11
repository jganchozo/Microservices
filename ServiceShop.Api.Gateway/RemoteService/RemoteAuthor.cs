using Ocelot.Responses;
using ServiceShop.Api.Gateway.RemoteBook;
using ServiceShop.Api.Gateway.RemoteInterface;
using System.Text.Json;

namespace ServiceShop.Api.Gateway.RemoteService
{
    public class RemoteAuthor : IRemoteAuthor
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<RemoteAuthor> _logger;

        public RemoteAuthor(IHttpClientFactory httpClientFactory, ILogger<RemoteAuthor> logger )
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool result, RemoteAuthorModel author, string errorMessage)> GetAuthor(Guid AuthorId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("AuthorService");
                var response = await client.GetAsync($"/author/{AuthorId}");

                if ( response.IsSuccessStatusCode )
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<RemoteAuthorModel>(content, options);

                    return (true, result, string.Empty);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
