using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using HealthCheckDemo.Contracts.Services;

namespace HealthCheckDemo.Services
{
    public class HttpClientService : IHttpClientService
    {
        private static readonly JsonSerializerOptions SerializationOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient _httpClient;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<T> GetAsync<T>(string endpoint, string? token)
        {
            var request = CreateHttpRequestMessage(HttpMethod.Get, endpoint, token);

            return await GetResponseAsync<T>(request);
        }

        public async Task<string> GetAsync(string endpoint, string? token)
        {
            var request = CreateHttpRequestMessage(HttpMethod.Get, endpoint, token);

            return await GetStringResponseAsync(request);
        }

        public async Task<T> PostJsonAsync<T>(string endpoint, string? token, object? data = null)
        {
            var request = CreateHttpRequestMessage(HttpMethod.Post, endpoint, token);
            if (data != null)
            {
                request.Content = GetJsonContent(data);
            }

            return await GetResponseAsync<T>(request);
        }

        public async Task<T> PostFormAsync<T>(string endpoint, string? token, Dictionary<string, string> data)
        {
            var request = CreateHttpRequestMessage(HttpMethod.Post, endpoint, token);
            if (data != null)
            {
                request.Content = GetFormUrlEncodedContent(data);
            }

            return await GetResponseAsync<T>(request);
        }

        private static HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string endpoint, string? token)
        {
            var request = new HttpRequestMessage(method, endpoint);

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return request;
        }

        private static StringContent GetJsonContent(object data)
        {
            var json = JsonSerializer.Serialize(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static FormUrlEncodedContent GetFormUrlEncodedContent(Dictionary<string, string> data)
        {
            return new FormUrlEncodedContent(data);
        }

        private async Task<T> GetResponseAsync<T>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(responseJson, SerializationOptions);

            return result!;
        }

        private async Task<string> GetStringResponseAsync(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();

            return responseJson;
        }
    }
}