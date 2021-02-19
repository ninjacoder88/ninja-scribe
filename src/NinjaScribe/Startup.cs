using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NinjaScribe.DataAccess;
using System;

namespace NinjaScribe
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IAzureMongoRepository, AzureMongoRepository>(t => new AzureMongoRepository(GetConnectionString("AzureMongo")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(cpb => cpb.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        private string GetConnectionString(string name)
        {
            if (_webHostEnvironment.IsProduction())
            {
                return Environment.GetEnvironmentVariable($"CUSTOMCONNSTR_{name}");
            }

            return _configuration.GetConnectionString(name);
        }

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
    }
}
