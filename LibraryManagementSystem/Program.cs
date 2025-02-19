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
           
            try
            {
                Log.Information("Starting up");
                var builder = WebApplication.CreateBuilder(args);
                builder.Host.UseSerilog(); // To Use Serilog instead of default .NET Logger


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

