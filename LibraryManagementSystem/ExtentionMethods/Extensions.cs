using LibraryManagementSystem.DataBaseConnection;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation;
using LibraryManagementSystem.Shared;
using FluentValidation.AspNetCore;
using LibraryManagementSystem.Shared.Validators;

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

            //Register The FluentValidation
            //Register "All" Validators In The Same Assembly
            services.AddControllers()
               .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AuthorDTOValidator>());

            // "Optionally", if we want to explicitly register each validator as well
            services.AddTransient<IValidator<AuthorDTO>, AuthorDTOValidator>();
            services.AddTransient<IValidator<BookDTO>, BookDTOValidator>();
            services.AddTransient<IValidator<MemberDTO>, MemberDTOValidator>();
            services.AddTransient<IValidator<GenreDTO>, GenreDTOValidator>();
            services.AddTransient<IValidator<BorrowedBookDTO>, BorrowedBookDTOValidator>();

            //2- Regist The Rest Of Needed Services

            services.AddScoped<IAuthorRepo, AuthorRepo>();
            services.AddScoped<IBookRepo, BookRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGenreRepo,GenreRepo>();
            services.AddScoped<IMemberRepo, MemberRepo>();
            services.AddScoped<IBorrowedBookRepo, BorrowedBookRepo>();

            //3 - Controllers
            // this is the old wich working with author and book
            //services.AddControllers()
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            //    // trying to handle serialization problem
            //});



            // //just to test NewtonsoftJson instead of AddJsonOptions
            // services.AddControllers()
            //.AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //    // You can configure more Newtonsoft.Json settings here if needed
            //});

            //last try
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
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
