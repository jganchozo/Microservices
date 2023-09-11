using ServiceShop.Api.Gateway.RemoteBook;
using ServiceShop.Api.Gateway.RemoteInterface;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace ServiceShop.Api.Gateway.MessageHandler
{
    public class BookHandler : DelegatingHandler
    {
        private readonly ILogger<BookHandler> _logger;
        private readonly IRemoteAuthor _remoteAuthor;

        public BookHandler(ILogger<BookHandler> logger, IRemoteAuthor remoteAuthor)
        {
            _logger = logger;
            _remoteAuthor = remoteAuthor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Stopwatch stopWatch = new();

            stopWatch.Start();
            _logger.LogInformation("Request started");

            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<RemoteBookModel>(content, options);

                //var authorResponse = await _remoteAuthor.GetAuthor(result.BookAuthor.Value);
                var authorResponse = await _remoteAuthor.GetAuthor(result?.BookAuthor ?? Guid.Empty);

                if (authorResponse.result && result is not null)
                {
                    result.Author = authorResponse.author;
                    var newContent = JsonSerializer.Serialize(result);
                    response.Content = new StringContent(newContent, Encoding.UTF8, "application/json");
                }
            }

            stopWatch.Stop();
            _logger.LogInformation($"This proccess took: {stopWatch.ElapsedMilliseconds} ms");

            return response;
        }
    }
}
