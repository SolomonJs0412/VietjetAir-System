using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.configs;
using flightdocs_system.models.http.http_request.Type;
using flightdocs_system.models.Type;

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

        public async Task<Boolean> SaveNewType(TypeInf request)
        {

            try
            {
                _dbContext.Add(request);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data sources" + ex.Message);
                return false;
            }
        }

        public dynamic FindGroupByCd(int cd)
        {

            try
            {
                var result = _dbContext.Types.FirstOrDefault(g => g.TypeCd == cd);
                if (result != null)
                {
                    return result;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data sources" + ex.Message);
                return false;
            }
        }

        public async Task<dynamic> SaveUpdate(NewTypeRequestTemplate updateInfo, TypeInf type)
        {
            var result = false;
            try
            {
                type.Name = updateInfo.Name;
                type.Note = updateInfo.Note;
                type.AccountCd = updateInfo.AccountCd;
                type.PermissionStr = updateInfo.PermissionStr;

                var getResponse = await _dbContext.SaveChangesAsync();
                if (getResponse == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data sources" + ex.Message);
                result = false;
            }
            return result;
        }
    }
}