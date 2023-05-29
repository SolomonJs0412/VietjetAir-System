using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.http.http_request.Documents
{
    public class UploadNewDocs
    {
        public int FlightCd { get; set; }
        public float Version { get; set; } = 1;
        public string Type { get; set; } = string.Empty;
        public List<string> Permission { get; set; } = new List<string>();
        public int GroupCd { get; set; }
        public Boolean isUpdate { get; set; }
        public int AccountCd { get; set; }
    }
}