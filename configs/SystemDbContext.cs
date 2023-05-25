using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.Account;
using Microsoft.EntityFrameworkCore;

namespace flightdocs_system.configs
{
    public class SystemDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options) { }

        public DbSet<AccountInfo> Accounts { get; set; }
    }
}