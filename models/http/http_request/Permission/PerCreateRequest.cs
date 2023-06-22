using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.http.http_request.Permission
{
    public class PerCreateRequest
    {
        public int typeCd { get; set; }
        public int permissions { get; set; }
        public int groupCd { get; set; }
    }
}