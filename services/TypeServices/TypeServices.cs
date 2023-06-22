using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.configs;

namespace flightdocs_system.services.TypeServices
{
    public class TypeServices
    {
        private readonly IConfiguration _config;

        private readonly SystemDbContext _dbContext;

        public TypeServices(IConfiguration configuration, SystemDbContext? dbContext)
        {
            _config = configuration;
            _dbContext = dbContext;
        }
    }
}