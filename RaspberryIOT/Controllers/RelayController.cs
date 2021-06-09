using Microsoft.AspNetCore.Mvc;
using RaspberryIOT.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Device.Gpio;
using System.Threading;

namespace RaspberryIOT.Controllers
{
    public class RelayController : Controller
    {
        GpioController gpioController;
        public RelayController(GpioController gpioController)
        {
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
                gpioController.Write(channel, PinValue.Low);
                return new ApiResponse() { Status = true, Message = channel + ". Kanal Açık." };
            }
            catch (Exception)
            {
                return new ApiResponse() { Status = false, Message = channel + ". Kanal Açılamadı." };
            }
        }
        [HttpGet]
        public ApiResponse RelayOFF(int channel)
        {
            try
            {
                gpioController.Write(channel, PinValue.High);
                return new ApiResponse() { Status = true, Message = channel + ". Kanal Kapatıldı." };
            }
            catch (Exception)
            {

                return new ApiResponse() { Status = false, Message = channel + ". Kanal Kapatılamadı." };

            }

        }
    }
}
