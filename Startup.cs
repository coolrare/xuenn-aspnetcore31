using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api1.Helpers;
using api1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace api1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // services.AddSingleton<IAppSettings, AppSettings>();

            // services.AddSingleton<IAppSettings>(new AppSettings() {
            //     TimeoutSeconds = 30,
            //     PartnerUrlBase = "https://mypartner.com.tw/"
            // });

            // var appSettings = new AppSettings();
            // Configuration.GetSection("AppSettings").Bind(appSettings);
            // services.AddSingleton<IAppSettings>(appSettings);

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddSingleton<JwtHelpers>();

            // Add OpenAPI v3 document
            services.AddOpenApiDocument();

            // Add Swagger v2 document
            // services.AddSwaggerDocument();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true; // Default: true

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Let "sub" assign to User.Identity.Name
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        // Let "roles" assign to Roles for [Authorized] attributes
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                        // Validate the Issuer
                        ValidateIssuer = true,
                        ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),

                        ValidateAudience = false,
                        //ValidAudience = "JwtAuthDemo", // TODO

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = false,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.EnvironmentName == "UAT")
            {
            }

            // app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseOpenApi();       // serve OpenAPI/Swagger documents

                app.UseSwaggerUi3();    // serve Swagger UI

                app.UseReDoc(config =>  // serve ReDoc UI
                {
                    // 這裡的 Path 用來設定 ReDoc UI 的路由 (網址路徑) (一定要以 / 斜線開頭)
                    config.Path = "/redoc";
                });
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
