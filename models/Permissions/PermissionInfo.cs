using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.Permissions
{
    public class PermissionInfo
    {
        [Key]
        public int PermissionCd { get; set; }
        public int TypeCd { get; set; }
        public int GroupCd { get; set; }
        public int PermissionsCd { get; set; }
        public string S3Content { get; set; } = string.Empty;
    }
}