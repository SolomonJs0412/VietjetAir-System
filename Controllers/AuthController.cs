using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Account;
using flightdocs_system.repositories.Account;
using flightdocs_system.staticObject.StaticResultResponse;
using Microsoft.AspNetCore.Mvc;

namespace flightdocs_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AuthController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("insertData")]
        public async Task<IActionResult> insertAccount([FromBody] CreateTemplate request)
        {
            CreateAccountResult result = new CreateAccountResult();

            try
            {
                string emailFormat = @"{0}@alta.com.vn";
                request.Email = String.Format(emailFormat, request.Email);
                result = await _accountRepository.CreateNewAccount(request);
                if (!result.isSuccess)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when insert account:" + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"ex.Message");
            }

            return CreatedAtAction(nameof(insertAccount), new { Username = request.Name }, result.Database);

        }
    }
}