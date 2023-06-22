using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Type;
using flightdocs_system.models.Type;
using flightdocs_system.services.TypeServices;
using flightdocs_system.staticObject.StaticResultResponse;

namespace flightdocs_system.repositories.Type
{
    public class TypeRepository : ITypeRepository
    {
        private readonly IConfiguration? _config;
        private readonly TypeServices _typeService;
        public TypeRepository(IConfiguration configuration, TypeServices typeService)
        {
            _config = configuration;
            _typeService = typeService;
        }

        public async Task<ServiceResponse> CreateType(NewTypeRequestTemplate request)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                TypeInf type = new TypeInf();
                type.Name = request.Name;
                type.Note = request.Note;
                type.PermissionStr = request.PermissionStr;
                type.CreatedAt = DateTime.Now;
                type.AccountCd = request.AccountCd;

                var saveResult = await _typeService.SaveNewType(type) ? true : false;
                if (saveResult)
                {
                    result.isSuccess = true;
                    result.StatusCode = 201;
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
        public async Task<ServiceResponse> UpdateType(NewTypeRequestTemplate request, int cd)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var type = _typeService.FindGroupByCd(cd);

                var saveResult = await _typeService.SaveUpdate(request, type); ;
                if (saveResult)
                {
                    result.isSuccess = true;
                    result.StatusCode = 201;
                    result.Database = type;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create type error:" + ex.Message);
                result.isSuccess = false;
                result.Message = ex.Message;
                result.StatusCode = 502;
            }
            return result;
        }
    }

}