using Microsoft.AspNetCore.Mvc;
using RaspberryIOT.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Device.Gpio;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace RaspberryIOT.Controllers
{
    public class RelayController : Controller
    {
        GpioController gpioController;
        IConfiguration config;
        public RelayController(GpioController gpioController, IConfiguration config)
        {
            this.config = config;
            this.gpioController = gpioController;
        }

        [HttpGet]
        public string ConnectionTest()
        {
            return "OK";
        }
        

        [HttpGet]
       public ApiResponse RelayON(int channel)
        {
            try
            {
                List<int> pins = config.GetSection("UsedPins:Pins").Get<List<int>>();
                if (pins.Where(x => x == channel).FirstOrDefault() == 0)
                {
                    return new ApiResponse() { Status = false, Message = channel + ". Kanal Açılamadı. Bu pine bağlı kanal yok"  };
                }
                gpioController.Write(channel, PinValue.Low);
                return new ApiResponse() { Status = true, Message = channel + ". Kanal Açık." };
            }
            catch (Exception ex )
            {
                return new ApiResponse() { Status = false, Message = channel + ". Kanal Açılamadı. Err:"+ex };
            }
        }
        [HttpGet]
        public ApiResponse RelayOFF(int channel)
        {
            try
            {
                List<int> pins = config.GetSection("UsedPins:Pins").Get<List<int>>();

                if (pins.Where(x => x == channel).FirstOrDefault() == 0)
                {
                    return new ApiResponse() { Status = false, Message = channel + ". Kanal Kapatılamadı. Bu pine bağlı kanal yok" };
                }
                gpioController.Write(channel, PinValue.High);
                return new ApiResponse() { Status = true, Message = channel + ". Kanal Kapatıldı." };
            }
            catch (Exception ex)
            {

                return new ApiResponse() { Status = false, Message = channel + ". Kanal Kapatılamadı. Err:" + ex };

            }

        }
    }
}
