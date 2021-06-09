using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RaspberryIOT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            IConfigurationRoot _config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
    .AddJsonFile("appsettings.json", false)
    .Build();
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                List<int> pins = _config.GetSection("UsedPins:Pins").Get<List<int>>();

                var gpioController = services.GetRequiredService<GpioController>();
                foreach (var item in pins)
                {
                    gpioController.OpenPin(item, PinMode.Output);
                    gpioController.Write(item, PinValue.Low);
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls("http://*:8000");
                });
    }
}
