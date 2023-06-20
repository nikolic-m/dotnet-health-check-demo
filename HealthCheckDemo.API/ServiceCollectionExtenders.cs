using HealthCheckDemo.API.Configurations;
using HealthCheckDemo.Data.Db;
using HealthCheckDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace HealthCheckDemo.API
{
    public static class ServiceCollectionExtenders
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.ConfigureHealthCheckDemoContext(configuration, environment.IsDevelopment());
            services.ConfigureHealthCheckDemoServices();

            services.AddHealthChecks(configuration);

            return services;
        }

        public static IApplicationBuilder Configure(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHealthChecks();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(configuration =>
            {
                configuration.MapControllers();
            });

            return app;
        }

        public static void ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HealthCheckDemoContext>();
                context.Database.Migrate();
            }
        }
    }
}
