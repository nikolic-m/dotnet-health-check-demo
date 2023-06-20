namespace HealthCheckDemo.ResourceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

            var app = builder.Build();
            app.Configure(app.Environment);

            app.Run();
        }
    }
}