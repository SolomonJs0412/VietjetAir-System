using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.Account;

namespace flightdocs_system.staticObject.StaticResultResponse
{
    public class CreateAccountResult
    {
        public bool isSuccess { get; set; }
        public AccountInfo? Database { get; set; }
    }
}