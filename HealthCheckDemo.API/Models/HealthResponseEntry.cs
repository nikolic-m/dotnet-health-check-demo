using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheckDemo.API.Models
{
    public class HealthResponseEntry
    {
        public IReadOnlyDictionary<string, object> Data { get; set; } = default!;

        public string? Description { get; set; }

        public TimeSpan Duration { get; set; }

        public HealthStatus Status { get; set; }

        public IEnumerable<string> Tags { get; set; } = default!;

        public static HealthResponseEntry FromHealthReportEntry(HealthReportEntry healthReportEntry)
        {
            return new HealthResponseEntry
            {
                Data = healthReportEntry.Data,
                Description = healthReportEntry.Description,
                Duration = healthReportEntry.Duration,
                Status = healthReportEntry.Status,
                Tags = healthReportEntry.Tags,
            };
        }
    }
}
