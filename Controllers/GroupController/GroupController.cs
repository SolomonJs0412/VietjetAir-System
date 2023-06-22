using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Group;
using flightdocs_system.repositories.Group;
using flightdocs_system.staticObject.StaticResultResponse;
using Microsoft.AspNetCore.Mvc;

namespace flightdocs_system.Controllers.GroupController
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpPost]
        [Route("new")]
        public async Task<dynamic> NewGroup([FromForm] GroupCreate request)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _groupRepository.CreateGroup(request);
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
                    result.Message = response.Message ? response.Message : "";
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
        [Route("update")]
        public async Task<dynamic> Update([FromForm] GroupCreate request, int cd)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _groupRepository.UpdateGroup(request, cd);
                if (response.StatusCode != 200)
                {
                    result.isSuccess = false;
                    result.Message = response.Message ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    return result;
                }
                else
                {
                    result.isSuccess = true;
                    result.Message = response.Message ? response.Message : "";
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
        public async Task<dynamic> GetGRoupByCd(int id)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _groupRepository.GetGroupByCd(id);
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
        public async Task<dynamic> DeleteGroup(int id)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _groupRepository.DeleteByCd(id);
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

        [HttpGet]
        [Route("get")]
        public async Task<dynamic> GetAllGRoup()
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var response = await _groupRepository.GetAllGRoup();
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