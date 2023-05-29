using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.Documents
{
    public class DocumentInfo
    {
        [Key]
        public int DocCd { get; set; }
        public string S3Key { get; set; } = string.Empty;
        public int FlightCd { get; set; }
        public double Version { get; set; } = 1;
        public string Type { get; set; } = string.Empty;
        public string Permission { get; set; } = string.Empty;
        public int GroupCd { get; set; }
    }
}