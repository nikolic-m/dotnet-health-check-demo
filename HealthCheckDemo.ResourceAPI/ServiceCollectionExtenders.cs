using HealthCheckDemo.ResourceAPI.Configurations;

namespace HealthCheckDemo.ResourceAPI
{
    public static class ServiceCollectionExtenders
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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
    }
}
