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
                if (response.StatusCode != 200)
                {
                    result.isSuccess = false;
                    result.Message = response.Message ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    return result;
                }
                else
                {
                    result.isSuccess = false;
                    result.Message = response.Message ? response.Message : "";
                    result.StatusCode = response.StatusCode;
                    result.Database = response.Database;
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
                    result.isSuccess = false;
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
    }
}