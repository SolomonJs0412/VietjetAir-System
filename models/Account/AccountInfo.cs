using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace flightdocs_system.models.Account
{
    public class AccountInfo
    {
        [Key]
        public int AccountCd { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; }
        public DateTime ExpiresTime { get; set; }

        public AccountInfo() { }
    }
}