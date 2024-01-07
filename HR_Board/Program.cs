using HR_Board.Data;
using HR_Board.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace HR_Board
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add DbContext
            var dbConnectionString = builder.Configuration.GetConnectionString("DbConnection");
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnectionString)); ;


            // Konfiguracja Identity

            /*builder.Services.AddIdentityApiEndpoints<ApiUser>(options =>
            {
                options.Password.RequireLowercase = true; // Wymagana mała litera
                options.Password.RequireUppercase = true; // Wymagana duża litera
                options.Password.RequireDigit = true; // Wymagana cyfra
                options.Password.RequireNonAlphanumeric = true; // Wymagany znak specjalny
                options.Password.RequiredLength = 8; // Minimalna długość: 8 znaków

            }).AddEntityFrameworkStores<AppDbContext>();*/

            builder.Services.AddIdentity<ApiUser, IdentityRole>(options =>
            {
                options.Password.RequireLowercase = true; // Wymagana mała litera
                options.Password.RequireUppercase = true; // Wymagana duża litera
                options.Password.RequireDigit = true; // Wymagana cyfra
                options.Password.RequireNonAlphanumeric = true; // Wymagany znak specjalny
                options.Password.RequiredLength = 8; // Minimalna długość: 8 znaków
            })
                .AddDefaultTokenProviders();*/


            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
            });

            builder.Services.AddAuthorization();

            //Swager configuration
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                opt.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapIdentityApi<ApiUser>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            

            app.MapControllers();

            app.Run();
        }
    }
}