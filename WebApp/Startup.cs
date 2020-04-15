using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Data;
using WebApp.Domain;

namespace WebApp
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
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../qa_client/dist";
            });
            services.AddControllers();
            var dataRepository = new DataRepository();
            services.AddSingleton<IQuestionsRepository>(dataRepository);
            services.AddSingleton<IUserRepository>(dataRepository);
            services.AddSingleton<IQuestionsService, App>();
            services.AddSingleton<IAuthService, App>();
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:8081")
                        .WithOrigins("http://localhost:5000")
                        .WithOrigins("https://localhost:5001");
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

            app.UseRouting();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("VueCorsPolicy");
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "../qa_client/src/";
                }
            );

        }
    }
}