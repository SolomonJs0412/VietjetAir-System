using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Account;
using flightdocs_system.repositories.Account;
using flightdocs_system.staticObject.StaticResultResponse;
using Microsoft.AspNetCore.Mvc;

namespace flightdocs_system.Controllers.sudo_controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateAdminUser : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public CreateAdminUser(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost]
        [Route("insertData")]
        public async Task<IActionResult> AdminAccount([FromForm] Boolean isCreate)
        {
            ServiceResponse result = new ServiceResponse();

            if (isCreate)
            {
                try
                {
                    CreateTemplate request = new CreateTemplate();
                    request.Name = "Ha My";
                    request.Email = "admin@vietjetair.com";
                    request.Password = "mely1234";
                    request.Role = "Admin";
                    request.PhoneNumber = "0987656754";
                    request.GroupCd = 1;
                    request.isActivate = 0;

                    var getResponse = await _accountRepository.CreateNewAccount(request);
                    if (!getResponse.isSuccess)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, $"");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when insert account:" + ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, $"ex.Message");
                }
            }
            else
            {
                Dictionary<string, string> account = new Dictionary<string, string>();
                account.Add("email", "admin@vietjetair.com");
                account.Add("password", "mely1234");
                result.Database = account;
            }


            return CreatedAtAction(nameof(AdminAccount), new { Username = "admin" }, result.Database);

        }
    }
}