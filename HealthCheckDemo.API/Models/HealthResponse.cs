using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheckDemo.API.Models
{
    public class HealthResponse
    {
        public HealthStatus Status { get; set; }

        public TimeSpan TotalDuration { get; set; }

        public Dictionary<string, HealthResponseEntry> Entries { get; set; } = default!;
    }
}
