using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.http.http_request.Type
{
    public class NewTypeRequestTemplate
    {
        public string Name { get; set; } = string.Empty;
        public int AccountCd { get; set; }
        public string PermissionStr { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}