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
                string emailFormat = @"{0}@vietjetair.com";
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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginTemplate request)
        {
            ServiceResponse result = new ServiceResponse();

            try
            {
                var user = await _accountRepository.Login(request);
                if (user.isSuccess == true)
                {
                    result.isSuccess = user.isSuccess;
                    result.Message = user.Message;
                    result.Database = user.Database;
                    result.StatusCode = user.StatusCode;

                    if (result.Database != null)
                    {
                        string token = "";
                        CookieOptions cookieOption = new CookieOptions();
                        if (user.Database.TryGetValue("token", out object value1))
                        {
                            token = value1 as string;
                        }
                        if (user.Database.TryGetValue("cookieOption", out object value2))
                        {
                            cookieOption = value2 as CookieOptions;
                        }

                        Response.Cookies.Append("RefreshToken", token, cookieOption);
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when login account:" + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"ex.Message");
            }

            return StatusCode(result.StatusCode, result.Database["token"] as string);

        }
    }
}