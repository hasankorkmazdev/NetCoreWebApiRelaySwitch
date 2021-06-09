using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RaspberryIOT.Utils;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;

namespace RaspberryIOT
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

            services.AddMvc();
            services.AddSingleton< GpioController>();
            services.AddSingleton< BasicAuth>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime,IServiceProvider servicePro)
        {
         

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              
            }
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                using (var serviceScope = servicePro.CreateScope())
                {
                    var services = serviceScope.ServiceProvider;
                    var gpioController = services.GetRequiredService<GpioController>();
                    foreach (var item in Program.usedPins)
                    {
                        gpioController.ClosePin(item);
                    }
                    gpioController.Dispose();
                }
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<AuthMiddleware>();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new {  controller = "Home", action = "Index" }
                    );
            });
        }
    }
}
