using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using flightdocs_system.models.Account;
using flightdocs_system.models.Documents;
using flightdocs_system.models.Group;
using flightdocs_system.models.Permissions;
using flightdocs_system.models.Type;
using Microsoft.EntityFrameworkCore;

namespace flightdocs_system.configs
{
    public class SystemDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options) { }

        public DbSet<AccountInfo> Accounts { get; set; }
        public DbSet<DocumentInfo> Documents { get; set; }
        public DbSet<GroupInfo> Groups { get; set; }
        public DbSet<PermissionInfo> Permissions { get; set; }
        public DbSet<TypeInf> Types { get; set; }
    }
}