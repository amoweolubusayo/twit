using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using tweetee.Infrastructure.Persistence;
using tweetee.Infrastructure.Swagger;
using tweetee.Infrastructure.Utility.Security;
using tweetee.Services;
using tweetee.Application.Entities;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using tweetee.Application.Commands;

namespace tweetee
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerService(Configuration);
            services.AddCors();
            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = "JwtBearer";
                 options.DefaultChallengeScheme = "JwtBearer";
             })
             .AddJwtBearer("JwtBearer", jwtBearerOptions =>
             {
                 jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = TokenConstants.ValidateSigningKey,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConstants.SecurityKey)),
                     ValidateIssuer = TokenConstants.ValidateIssuer,
                     ValidateAudience = TokenConstants.ValidateAudience,
                     ValidAudience = TokenConstants.Audience,
                     ClockSkew = TimeSpan.Zero
                 };
             });

            services.Configure<GeneralSettings>(Configuration.GetSection("GeneralSettings"));

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            services.AddPersistence(Configuration);
            services.AddMvcCore()
                .AddApiExplorer();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                                builder =>
                                                {
                                                    builder
                                                                    .AllowAnyHeader()
                                                                    .AllowAnyMethod()
                                                                    .AllowAnyOrigin();
                                                });

                //     // In production, the Angular files will be served from this directory
                //     // services.AddSpaStaticFiles(configuration =>
                //     // {
                //     //     configuration.RootPath = "ClientApp/dist";
                //     // });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwaggerService(Configuration);
            app.UseRouting();
            app.UseCors();  
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                // app.UseSpa(spa =>
                // {
                //     // To learn more about options for serving an Angular SPA from ASP.NET Core,
                //     // see https://go.microsoft.com/fwlink/?linkid=864501

                //     spa.Options.SourcePath = "ClientApp";

                //     if (env.IsDevelopment())
                //     {
                //         spa.UseAngularCliServer(npmScript: "start");
                //     }
                // });
            });
          
            app.UseMiddleware<JwtMiddleware>();
            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
          


        }
    }
}
