using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Permission;

namespace flightdocs_system.repositories.Permission
{
    public interface IPermissionRepository
    {
        Task<dynamic> NewPermission(List<PerCreateRequest> request, string S3FolderName);
    }
}