using IPO.API.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;

namespace IPO.API.Services
{
    public class UpstoxApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly UpstoxOptions _options;

        public UpstoxApiClient(
            HttpClient httpClient,
            IOptions<UpstoxOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;

            _httpClient.BaseAddress =
                new Uri(_options.BaseUrl);

            _httpClient.DefaultRequestHeaders.Accept.Clear();

            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/json"));
        }

        public async Task<JsonDocument> GetAsync(
     string relativeUrl,
     CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                relativeUrl);

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _options.AccessToken);

            var response = await _httpClient.SendAsync(
                request,
                cancellationToken);

            var content =
                await response.Content.ReadAsStringAsync(
                    cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Upstox API Error: {(int)response.StatusCode} " +
                    $"- {content}\n" +
                    $"Request URL: {request.RequestUri}");
            }

            return JsonDocument.Parse(content);
        }
    }
}
