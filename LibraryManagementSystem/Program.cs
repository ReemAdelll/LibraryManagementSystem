
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static LibraryManagementSystem.Repositories.IGenericRepo;

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
			builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
			builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
			builder.Services.AddScoped<IBookRepo, BookRepo>();



			builder.Services.AddControllers();
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
