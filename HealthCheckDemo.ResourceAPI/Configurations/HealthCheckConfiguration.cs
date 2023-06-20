using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheckDemo.ResourceAPI.Configurations
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck("Sample", () => HealthCheckResult.Healthy("A healthy result"));
            //.AddCheck("Sample", () => HealthCheckResult.Degraded("A degraded result"));
            //.AddCheck("Sample", () => HealthCheckResult.Unhealthy("An unhealthy result"));

            return services;
        }

        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health");

            return app;
        }
    }
}
