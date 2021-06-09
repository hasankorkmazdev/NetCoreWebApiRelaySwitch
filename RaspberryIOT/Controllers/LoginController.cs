using Microsoft.AspNetCore.Mvc;
using RaspberryIOT.Response;
using RaspberryIOT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaspberryIOT.Controllers
{
    public class LoginController : Controller
    {
        BasicAuth auther;
        public LoginController(BasicAuth auther)
        {
            this.auther = auther;
        }
        [HttpPost]
        public ApiResponse Login(string userName,string password)
        {
            Guid? authKey=Guid.Empty;
            if (userName=="Admin" && password=="delta")
            {
                authKey=auther.CreateAuthKey();
                return new ApiResponse() { Status = true, Message = authKey.Value.ToString() };
            }
            else
            {
                return new ApiResponse() { Status = false, Message = "Oturum Açılamadı." };

            }
        }
        public ApiResponse Logout([FromHeader]Guid authToken)
        {
            var response=auther.RemoveAuthKey(authToken);
            if (response)
            {
                return new ApiResponse() { Status = true, Message = "Başarılı bir şekilde çıkış yaptınız." };
            }
            else
            {
                return new ApiResponse() { Status = false, Message = "Güvenli çıkış yapılamadı ." };

            }
        }
    }
}
