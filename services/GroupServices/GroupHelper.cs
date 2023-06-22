using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.configs;
using flightdocs_system.models.Group;
using flightdocs_system.models.http.http_request.Group;

namespace flightdocs_system.services.GroupServices
{
    public class GroupHelper
    {
        private readonly IConfiguration _config;

        private readonly SystemDbContext _dbContext;

        public GroupHelper(IConfiguration configuration, SystemDbContext? dbContext)
        {
            _config = configuration;
            _dbContext = dbContext;
        }
        public async Task<Boolean> SaveNewGroup(GroupInfo request)
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
                var result = _dbContext.Groups.FirstOrDefault(g => g.GroupCd == cd);
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
        public List<GroupInfo> GetAll()
        {
            var result = new List<GroupInfo>();
            try
            {
                result = _dbContext.Groups.ToList();
                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data sources" + ex.Message);
            }
            return result;
        }
        public async Task<dynamic> DeleteGroup(GroupInfo cd)
        {
            var result = true;
            try
            {
                var getRsl = _dbContext.Groups.Remove(cd);
                await _dbContext.SaveChangesAsync();
                if (getRsl != null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine("Error saving data sources" + ex.Message);
            }
            return result;
        }

        public async Task<dynamic> SaveUpdate(GroupCreate updateInfo, GroupInfo group)
        {
            var result = false;
            try
            {
                group.GroupName = updateInfo.GroupName;
                group.Note = updateInfo.Note;
                group.UserInsCd = updateInfo.UserInsCd;

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