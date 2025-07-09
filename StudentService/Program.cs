using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentService.Aspects;
using StudentService.Data;
using StudentService.Repository;
using StudentService.Services;
using System.Configuration;

namespace StudentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<StudentServiceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StudentServiceContext") ?? throw new InvalidOperationException("Connection string 'StudentServiceContext' not found.")));
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithHeaders("Accept", "Content-Type", "Origin", "X-My-Header"));
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StudentServiceContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("StudentServiceContext")));
            builder.Services.AddScoped<IStudRepository, StudentRepository>();
            builder.Services.AddScoped<IStudService, StudService>();
            builder.Services.AddScoped<ExceptionHandlerAttribute>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyCorsPolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
