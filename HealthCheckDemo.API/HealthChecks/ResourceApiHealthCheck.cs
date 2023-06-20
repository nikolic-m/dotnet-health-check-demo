using HealthCheckDemo.Contracts.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheckDemo.API.HealthChecks
{
    public class ResourceApiHealthCheck : IHealthCheck
    {
        public const string Name = "Resource API check";

        private readonly IHttpClientService _httpClientService;

        public ResourceApiHealthCheck(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var result = await _httpClientService.GetAsync("https://localhost:7053/health", null);

            if (string.IsNullOrWhiteSpace(result) || !result.Equals("Healthy", StringComparison.OrdinalIgnoreCase))
            {
                return HealthCheckResult.Degraded("Resource API reported unhealthy status!");
            }
            else
            {
                return HealthCheckResult.Healthy(result);
            }
        }
    }
}
