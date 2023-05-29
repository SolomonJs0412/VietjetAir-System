using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.configs;
using flightdocs_system.models.Documents;

namespace flightdocs_system.services.DocumentServices
{
    public class DocumentSevices
    {
        private readonly IConfiguration _config;

        private readonly SystemDbContext _dbContext;

        public DocumentSevices(IConfiguration configuration, SystemDbContext? dbContext)
        {
            _config = configuration;
            _dbContext = dbContext;
        }

        public async Task<Boolean> SaveDocuments(DocumentInfo doc)
        {
            try
            {
                _dbContext.Add(doc);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data sources" + ex.Message);
                return false;
            }
        }
    }
}