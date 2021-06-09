using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaspberryIOT.Response
{
    public class ApiResponse
    {
        public ApiResponse()
        {
        }
        public ApiResponse(bool status, string message,int code=200)
        {
            this.Status = status;
            this.Message = message;
            this.Code = code;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
