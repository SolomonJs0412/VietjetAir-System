using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.models.Group
{
    public class GroupInfo
    {
        [Key]
        public int GroupCd { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int UserInsCd { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public GroupInfo() { }
    }
}