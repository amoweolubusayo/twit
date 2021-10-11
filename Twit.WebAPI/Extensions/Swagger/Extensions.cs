using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
namespace Twit.WebAPI.Swagger
{
   
     public static class Extensions
    {
         public static IServiceCollection AddSwaggerService(this IServiceCollection services,
            IConfiguration configuration)
        {
            string ApplicationName = configuration["Swagger:ApplicationName"];

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{ApplicationName} API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder builder, IConfiguration configuration)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", $"{configuration["Swagger:Title"]} API V1");
            });

            return builder;
        }
    }
    }
