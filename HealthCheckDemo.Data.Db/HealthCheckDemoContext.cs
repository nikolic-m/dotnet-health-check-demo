using Microsoft.EntityFrameworkCore;

namespace HealthCheckDemo.Data.Db
{
    public class HealthCheckDemoContext : DbContext
    {
        public HealthCheckDemoContext()
        {
        }

        public HealthCheckDemoContext(DbContextOptions<HealthCheckDemoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(HealthCheckDemoContext).Assembly);
        }
    }
}
