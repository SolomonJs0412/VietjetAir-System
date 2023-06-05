using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.Group;
using flightdocs_system.models.http.http_request.Group;
using flightdocs_system.services.GroupServices;
using flightdocs_system.staticObject.StaticResultResponse;

namespace flightdocs_system.repositories.Group
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IConfiguration? _config;
        private readonly GroupHelper _groupService;

        public GroupRepository(IConfiguration configuration, GroupHelper groupService)
        {
            _config = configuration;
            _groupService = groupService;
        }

        public async Task<dynamic> CreateGroup(GroupCreate request)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                GroupInfo group = new GroupInfo();
                group.GroupName = request.GroupName;
                group.Note = request.Note;
                group.UserInsCd = request.UserInsCd;

                var saveResult = await _groupService.SaveNewGroup(group) ? true : false;
                if (saveResult)
                {
                    result.isSuccess = true;
                    result.StatusCode = 201;
                    result.Database = group;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create group error:" + ex.Message);
                result.isSuccess = false;
                result.Message = ex.Message;
                result.StatusCode = 502;
            }
            return result;
        }


        public async Task<dynamic> UpdateGroup(GroupCreate request, int groupCd)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var group = _groupService.FindGroupByCd(groupCd);

                var saveResult = await _groupService.SaveUpdate(request, group); ;
                if (saveResult)
                {
                    result.isSuccess = true;
                    result.StatusCode = 201;
                    result.Database = group;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create group error:" + ex.Message);
                result.isSuccess = false;
                result.Message = ex.Message;
                result.StatusCode = 502;
            }
            return result;
        }
    }
}