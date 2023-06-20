using HealthCheckDemo.API;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

        var app = builder.Build();
        app.Services.ApplyMigrations();

        app.Configure(app.Environment);

        app.Run();
    }
}