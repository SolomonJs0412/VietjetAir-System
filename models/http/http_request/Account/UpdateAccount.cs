using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.http.http_request.Account
{
    public class UpdateAccount
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int GroupCd { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}