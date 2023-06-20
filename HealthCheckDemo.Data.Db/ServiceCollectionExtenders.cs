using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthCheckDemo.Data.Db
{
    public static class ServiceCollectionExtenders
    {
        public static IServiceCollection ConfigureHealthCheckDemoContext(this IServiceCollection services, IConfiguration configuration, bool enableDetailedErrors)
        {
            var connectionString = configuration.GetConnectionString("HealthCheckDemo");

            services.AddDbContext<HealthCheckDemoContext>(options =>
            {
                options.UseNpgsql(connectionString, opt => opt.MigrationsAssembly(typeof(HealthCheckDemoContext).Assembly.FullName));

                if (enableDetailedErrors)
                {
                    options
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                }
            });

            return services;
        }
    }
}