
using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;


namespace LibraryManagementSystem
{
	public class Program
	{

		public static void Main(string[] args)
		{

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.


			//1- Regist The DbContext
			builder.Services.AddDbContext<LibraryContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("Conn1")));

			//2- Regist The Rest Of Needed Services
			
			builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
			builder.Services.AddScoped<IBookRepo, BookRepo>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



			//builder.Services.AddControllers();
			builder.Services.AddControllers()
	   .AddJsonOptions(options =>
	   {
		   options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
		   options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
	   });
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library Management API", Version = "v1" });
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
