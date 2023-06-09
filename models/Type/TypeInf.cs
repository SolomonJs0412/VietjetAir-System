using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.Type
{
    public class TypeInf
    {
        [Key]
        public int TypeCd { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int AccountCd { get; set; }
        public string PermissionStr { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}