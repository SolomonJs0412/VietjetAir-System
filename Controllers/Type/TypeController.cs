using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Type;
using flightdocs_system.repositories.Type;
using flightdocs_system.staticObject.StaticResultResponse;
using Microsoft.AspNetCore.Mvc;

namespace flightdocs_system.Controllers.Type
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController : ControllerBase
    {
        private readonly ITypeRepository _typeRepository;
        public TypeController(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }

        [HttpPost]
        [Route("new")]
        public async Task<dynamic> NewType([FromForm] NewTypeRequestTemplate request)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _typeRepository.CreateType(request);
                if (response.StatusCode != 201)
                {
                    result.isSuccess = false;
                    result.Message = (response.Message != null) ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.Message = (response.Message != null) ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal error: " + ex.Message);
            }
            return result;
        }

        [HttpPut]
        [Route("update/{cd}")]
        public async Task<dynamic> Update([FromForm] NewTypeRequestTemplate request, int cd)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _typeRepository.UpdateType(request, cd);
                if (response.StatusCode != 200)
                {
                    result.isSuccess = false;
                    result.Message = (response.Message != null) ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.Message = (response.Message != null) ? response.Message : "";
                    result.StatusCode = 201;
                    result.Database = response.Database;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal error: " + ex.Message);
            }
            return result;
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<dynamic> GetTypeByCd(int id)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _typeRepository.GetTypeByCd(id);
                if (response.StatusCode == 502)
                {
                    result.isSuccess = false;
                    result.Message = (response.Message != null) ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    result.Database = response.Database;
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.StatusCode = 201;
                    result.Database = response.Database;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal error: " + ex.Message);
            }
            return result;
        }

        [HttpGet]
        [Route("get")]
        public async Task<dynamic> GetAllType()
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _typeRepository.GetAllType();
                if (response.StatusCode == 502)
                {
                    result.isSuccess = false;
                    result.Message = (response.Message != null) ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.StatusCode = 201;
                    result.Database = response.Database;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal error: " + ex.Message);
            }
            return result;
        }

        [HttpDelete]
        [Route("bye/{id}")]
        public async Task<dynamic> DeleteType(int id)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _typeRepository.DeleteByCd(id);
                if (response.StatusCode == 502)
                {
                    result.isSuccess = false;
                    result.Message = (response.Message != null) ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.StatusCode = 201;
                    result.Database = response.Database;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Internal error: " + ex.Message);
            }
            return result;
        }
    }
}