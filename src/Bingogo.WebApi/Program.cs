using Bingogo.Data;
using Bingogo.Services;
using Bingogo.WebApi.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Bingogo.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
            {
                Args = args,
                ContentRootPath = AppContext.BaseDirectory
            });

            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "Bingogo API", Version = "v1" });
            });

            // Health 
            builder.Services
                .AddHealthChecks()
                .AddDbContextCheck<DbContext>();

            builder.Services.AddMemoryCache();

            builder.Services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudience = builder.Configuration["Auth:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Auth:SecretKey"])),
                    };
                });

            builder.Services.AddHttpContextAccessor();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true);
                });
            });

            builder.Services.AddAppServices();
            builder.Services.AddAppDb();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            // Controllers 
            app.MapControllers();

            // Middleware
            // app.UseHttpsRedirection();
            app.UseHealthChecks("/");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            // Error Handling
            var exceptionHandler = new ExceptionHandler
            {
                IncludeExceptionDetails = !builder.Environment.IsProduction() //
            };
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = exceptionHandler.Handle //
            });

            app.Run();
        }
    }
}
