using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OwnThings.Core.Data;
using OwnThings.Core.Models;
using OwnThings.Core.Repositories.Interfaces;
using OwnThings.Core.Repositories.Mocks;

namespace OwnThings.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options => {
              options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));
            services.AddDbContext<OwnThingsContext>(options => options.UseSqlServer(Configuration.GetValue<string>("ApplicationSettings:ConnectionString")));

            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IMeasurementRepository, MeasurementRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("ApplicationSettings:JWTSecret"));
            services.AddAuthentication(m =>
            {
                m.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                m.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(m =>
            {
                m.RequireHttpsMetadata = false;
                m.SaveToken = true;
                m.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            // Swagger Config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OwnThings Cloud API",
                    Description = "OwnThings Cloud is a simple iot cloud API"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddOptions();

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            string path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            // Swagger configs
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OwnThings Cloud API V1");
                c.RoutePrefix = string.Empty;
            });
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/api/health");
            });
        }
    }
}
