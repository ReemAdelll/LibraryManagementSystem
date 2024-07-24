//using LibraryManagementSystem.ExtentionMethods;
//using Microsoft.AspNetCore.Builder;
//using Serilog;
//using Serilog.Events;


//namespace LibraryManagementSystem
//{
//    public class Program
//    {

//        public static void Main(string[] args)
//        {


//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            builder.Services.ServicesRegistration(builder.Configuration);

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            app.UseMiddlewares(app.Environment);

//            app.Run();
//        }
//    }
//}
using LibraryManagementSystem.ExtentionMethods;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace LibraryManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting up");
                var builder = WebApplication.CreateBuilder(args);

                //builder.Host.UseSerilog();

                // Add services to the container.
                builder.Services.ServicesRegistration(builder.Configuration);

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                app.UseMiddlewares(app.Environment);

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}

