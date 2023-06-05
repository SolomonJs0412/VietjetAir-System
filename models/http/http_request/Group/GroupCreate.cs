using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.http.http_request.Group
{
    public class GroupCreate
    {
        public string GroupName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int UserInsCd { get; set; }
    }
}