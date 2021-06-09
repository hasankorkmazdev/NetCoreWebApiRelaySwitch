using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Threading.Tasks;

namespace RaspberryIOT
{
    public class Program
    {
        public static List<int>   usedPins = new List<int>() { 2, 3, 4, 17 };
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var gpioController = services.GetRequiredService<GpioController>();
                foreach (var item in usedPins)
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
                    webBuilder.UseStartup<Startup>();
                });
    }
}
