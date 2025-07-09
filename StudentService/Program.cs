using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudentService.Aspects;
using StudentService.AuthFolder;
using StudentService.Data;
using StudentService.Repository;
using StudentService.Services;
using System.Configuration;
using System.Text;

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
            var key = "This_is_my_first_Test_Key_for_jwt_token";
            builder.Services.AddControllers();
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("This_is_my_first_Test_Key_for_jwt_token"))
                };
            });

            builder.Services.AddScoped<IAuth>(provider =>
            {
                var context = provider.GetRequiredService<StudentServiceContext>();
                var key = "This_is_my_first_Test_Key_for_jwt_token";
                return new Auth( key, context);
            });
           // builder.Services.AddSingleton<IAuth>(new Auth("This_is_my_first_Test_Key_for_jwt_token",        ));

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
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
