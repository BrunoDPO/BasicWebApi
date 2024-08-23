using Microsoft.AspNetCore.Builder;
using Serilog;

namespace BrunoDPO.BasicAPI.WebApi
{
    static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((hostContext, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(hostContext.Configuration);
            });
            Startup startup = new(builder.Configuration);
            startup.ConfigureServices(builder.Services);
            WebApplication app = builder.Build();
            startup.Configure(app, app.Environment);
            app.Run();
        }
    }
}
