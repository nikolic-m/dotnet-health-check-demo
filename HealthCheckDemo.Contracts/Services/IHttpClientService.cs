namespace HealthCheckDemo.Contracts.Services
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string endpoint, string? token);

        Task<string> GetAsync(string endpoint, string? token);

        Task<T> PostJsonAsync<T>(string endpoint, string? token, object? data = null);

        Task<T> PostFormAsync<T>(string endpoint, string? token, Dictionary<string, string> data);
    }
}
