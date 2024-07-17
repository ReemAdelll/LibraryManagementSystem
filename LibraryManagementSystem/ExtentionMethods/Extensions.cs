using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
namespace LibraryManagementSystem.ExtentionMethods

{
    public static class Extensions
    {
          //Services Extention Method
          public static IServiceCollection ServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            //1- Regist The DbContext
            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Conn1")));

            //2- Regist The Rest Of Needed Services

            services.AddScoped<IAuthorRepo, AuthorRepo>();
            services.AddScoped<IBookRepo, BookRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //3- Controllers
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            //4- Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library Management API", Version = "v1" });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        //middle ware extention method
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app, IHostEnvironment env) 
        {
            if (env.IsDevelopment())
            { 
                app.UseSwagger();
				app.UseSwaggerUI();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryManagementSystem v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
           app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
            return app;
        }
    }
}
