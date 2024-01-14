﻿using HR_Board.Config;
using HR_Board.Data;
using HR_Board.Services;
using HR_Board.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace HR_Board
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<JwtTokenSettings>(builder.Configuration.GetSection("JwtTokenSettings"));
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:DbConnection")));

            // Konfiguracja Identity

            builder.Services.AddScoped<JWTTokenService>();

            builder.Services.AddIdentity<ApiUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireLowercase = true; // Wymagana mała litera
                options.Password.RequireUppercase = true; // Wymagana duża litera
                options.Password.RequireDigit = true; // Wymagana cyfra
                options.Password.RequireNonAlphanumeric = true; // Wymagany znak specjalny
                options.Password.RequiredLength = 8; // Minimalna długość: 8 znaków
            })
            .AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ReportApiVersions = true;
            });


            var jwtSettings = new JwtTokenSettings();
            builder.Configuration.GetSection("JwtTokenSettings").Bind(jwtSettings);

            // Set the default authentication scheme
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>(jwtSettings.SymmetricSecurityKey)!)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
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

            app.UseHttpsRedirection();;
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}