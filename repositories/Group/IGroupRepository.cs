using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Group;

namespace flightdocs_system.repositories.Group
{
    public interface IGroupRepository
    {
        Task<dynamic> CreateGroup(GroupCreate request);
        Task<dynamic> UpdateGroup(GroupCreate request, int groupCd);
    }
}