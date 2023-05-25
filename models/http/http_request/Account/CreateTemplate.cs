using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.http.http_request.Account
{
    public class CreateTemplate
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}