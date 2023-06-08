using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Projekat.Api.Core;
using Projekat.Application.Emails;
using Projekat.Application.Logging;
using Projekat.Application.UseCases;
using Projekat.EfDataAccess;
using Projekat.EfDataAccess.Configuration;
using Projekat.Implementation.Email;
using Projekat.Implementation.Logging;
using Projekat.Implementation.UseCases.UseCaseLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat.Api
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
            var appSettings = new AppSettings();

            Configuration.Bind(appSettings);

            services.AddSingleton(appSettings);
            services.AddControllers();
            services.AddTransient<ProjekatContext>();
            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();
            services.AddTransient<IExceptionLogger, ConsoleExceptionLogger>();
            services.AddTransient<CategoryConfiguration>();
            services.AddTransient<PostConfiguration>();

            services.AddUsesCases();

            services.AddHttpContextAccessor();
            services.AddApplicationUser();
            services.AddJwt(appSettings);

            services.AddTransient<IEmailSender>(x =>
            new SmtpEmailSender(appSettings.EmailOptions.EmailFrom,
                                appSettings.EmailOptions.EmailPassword,
                                appSettings.EmailOptions.EmailPort,
                                appSettings.EmailOptions.EmailHost));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projekat.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,

                            },
                            new List<string>()
                          }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projekat.Api v1"));

            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
