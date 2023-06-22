using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Permission;
using flightdocs_system.services.PermissionServices;
using flightdocs_system.staticObject.StaticResultResponse;
using flightdocs_system.Utils.S3;

namespace flightdocs_system.repositories.Permission
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IConfiguration? _config;
        private readonly PermissionHelper _permissionService;
        public readonly IS3Utils _s3;

        public PermissionRepository(IConfiguration configuration, PermissionHelper permissionService, IS3Utils s3)
        {
            _config = configuration;
            _permissionService = permissionService;
            _s3 = s3;
        }

        public async Task<dynamic> NewPermission(List<PerCreateRequest> request, string S3FolderName)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var createFolderResponse = await _s3.CreateFolder(S3FolderName, "flightdocs-demo");
                if (createFolderResponse)
                {
                    var uploadResponse = await _s3.CreateJsonFileAsync(S3FolderName, request);
                    if (uploadResponse)
                    {
                        result.StatusCode = 201;
                        result.isSuccess = true;
                        result.Database = S3FolderName;
                    }
                    else
                    {
                        result.StatusCode = 502;
                        result.isSuccess = false;
                        result.Database = S3FolderName;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create new Permission Erro: " + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 502;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}