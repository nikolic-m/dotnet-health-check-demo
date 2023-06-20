using HealthCheckDemo.API.HealthChecks;
using HealthCheckDemo.API.Models;
using HealthCheckDemo.Data.Db;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace HealthCheckDemo.API.Configurations
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck("Sample", () => HealthCheckResult.Healthy("A healthy result"))
                .AddDbContextCheck<HealthCheckDemoContext>()
                .AddCheck<ResourceApiHealthCheck>(ResourceApiHealthCheck.Name);

            return services;
        }

        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            //app.UseHealthChecks("/health");

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = WriteResponse
            });

            return app;
        }

        private static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            options.Converters.Add(new JsonStringEnumConverter());

            var healthResponse = new HealthResponse
            {
                Status = healthReport.Status,
                TotalDuration = healthReport.TotalDuration,
                Entries = new Dictionary<string, HealthResponseEntry>()
            };

            foreach (var entry in healthReport.Entries)
            {
                healthResponse.Entries.Add(entry.Key, HealthResponseEntry.FromHealthReportEntry(entry.Value));
            }

            var json = JsonSerializer.Serialize(
                healthResponse,
                options);

            context.Response.ContentType = "application/json;";

            return context.Response.WriteAsync(json);
        }
    }
}
