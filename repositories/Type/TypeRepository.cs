using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.common;
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
        public readonly StringSupport _stringSp;
        public TypeRepository(IConfiguration configuration, TypeServices typeService, StringSupport stringSp)
        {
            _config = configuration;
            _typeService = typeService;
            _stringSp = stringSp;
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

        public async Task<ServiceResponse> GetTypeByCd(int typecd)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var type = _typeService.FindGroupByCd(typecd);
                if (type != null)
                {
                    Dictionary<string, object> rls = new Dictionary<string, object>();
                    var permission = _stringSp.SliceString(type.PermissionStr);

                    rls.Add("datatable", type);
                    rls.Add("permission", permission);

                    result.isSuccess = true;
                    result.StatusCode = 201;
                    result.Database = rls;
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

        public async Task<ServiceResponse> GetAllType()
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var type = _typeService.GetAll();
                if (type != null)
                {
                    result.isSuccess = true;
                    result.StatusCode = 201;
                    result.Database = type;
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

        public async Task<ServiceResponse> DeleteByCd(int groupCd)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var type = _typeService.FindGroupByCd(groupCd);
                if (type != null)
                {
                    var getResult = _typeService.DeleteType(type);
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