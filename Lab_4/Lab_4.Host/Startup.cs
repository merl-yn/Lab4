using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_4.DataAccess.Implementation;
using Lab_4.Domain.Abstraction;
using Lab_4.Domain.Entity;
using Lab_4.Domain.Service;
using Lab_4.Host.Cfg;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Lab_4.Host
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Lab_4.Host", Version = "v1"});
            });

            services.AddMediatR(typeof(IRepository<>).Assembly)
                .AddSingleton<IRepository<StudentEntity>, StudentsRepository>()
                .Configure<AppConfig>(Configuration.GetSection("appConfig"))
                .AddSingleton<IHostelService, HostelService>()
                .AddSingleton<IResidentService, ResidentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab_4.Host v1"));
            
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}