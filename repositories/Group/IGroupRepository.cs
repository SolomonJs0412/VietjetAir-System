using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Group;
using flightdocs_system.staticObject.StaticResultResponse;

namespace flightdocs_system.repositories.Group
{
    public interface IGroupRepository
    {
        Task<dynamic> CreateGroup(GroupCreate request);
        Task<dynamic> UpdateGroup(GroupCreate request, int groupCd);
        Task<ServiceResponse> GetGroupByCd(int groupCd);
        Task<ServiceResponse> GetAllGRoup();

        Task<ServiceResponse> DeleteByCd(int groupCd);
    }
}