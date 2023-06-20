using HealthCheckDemo.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCheckDemo.Services
{
    public static class ServiceCollectionExtenders
    {
        public static IServiceCollection ConfigureHealthCheckDemoServices(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<IHttpClientService, HttpClientService>();

            return services;
        }
    }
}
