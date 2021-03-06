using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using RaspberryIOT.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RaspberryIOT.Utils
{
    public class AuthMiddleware

    {
        private readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext,BasicAuth auther)
        {
            
            /**************************/
            /**                      **/
            /**                      **/
            /**    HK Basic Auth     **/
            /**        System        **/
            /**                      **/
            /**                      **/
            /**************************/
            
            var controllerActionDescriptor = httpContext.GetEndpoint();
            if (controllerActionDescriptor==null)
            {
                await _next(httpContext);

            }
            else
            {
                var varMetaData = controllerActionDescriptor.Metadata.GetMetadata<ControllerActionDescriptor>();

                var controllerName = varMetaData.ControllerName;
                var actionName = varMetaData.ActionName;
                var authToken = httpContext.Request.Headers.FirstOrDefault(x=> x.Key== "authToken");
                if ((controllerName.ToLower() == "login" && actionName.ToLower() == "login") || (controllerName.ToLower() == "login" && actionName.ToLower() == "logout"))
                {
                    //Logine Geliyorsa bırak geçsin
                    await _next(httpContext);

                }
                else
                {
                    if (authToken.Key == null)
                    {
                        //Diğer Noktalara Geliyorsa ve tokeni yoksa  Engelle 

                        var json = JsonConvert.SerializeObject(new ApiResponse() { Status = false, Code = 203, Message = "Oturum Açmanız Gerekiyor." });
                        await httpContext.Response.WriteAsync(json);

                    }
                    else
                    {
                        //Diğer Noktalara Geliyorsa ve tokeni varsa  Yetkisi varmı bak varsa  geçsin 
                        Console.WriteLine("Token" + authToken);
                        Console.WriteLine("Guid Token"+Guid.Parse(authToken.Value));
                        foreach (var item in auther.Authorized)
                        {
                            Console.WriteLine("TokenList" + item);

                        }
                        if (!auther.isAuth(Guid.Parse(authToken.Value)))
                        {
                            var json = JsonConvert.SerializeObject(new ApiResponse() { Status = false, Code = 203, Message = "Api Key Geçersiz. Yeniden Oturum Açmanız Gerekiyor." });
                            await httpContext.Response.WriteAsync(json);
                        }
                        else
                        {
                            await _next(httpContext);
                        }

                    }
                }
               
            }
            

        }
    }
}
