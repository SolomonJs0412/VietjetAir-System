using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.staticObject.StaticResultResponse
{
    public class ServiceResponse
    {
        public bool isSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public dynamic? Database { get; set; }
    }
}