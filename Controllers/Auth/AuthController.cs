using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Account;
using flightdocs_system.repositories.Account;
using flightdocs_system.staticObject.StaticResultResponse;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
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
                        string refreshToken = "";
                        CookieOptions cookieOption = new CookieOptions();
                        if (user.Database.TryGetValue("token", out object value1))
                        {
                            token = value1 as string;
                        }
                        if (user.Database.TryGetValue("cookieOption", out object value2))
                        {
                            cookieOption = value2 as CookieOptions;
                        }
                        if (user.Database.TryGetValue("refresh-token", out object value3))
                        {
                            refreshToken = value3 as string;
                        }

                        Response.Cookies.Append("RefreshToken", refreshToken, cookieOption);
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
        [HttpGet]
        [Route("me")]
        public async Task<ActionResult<dynamic>> Me()
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                string token = new String("");
                var cookie = HttpContext.Request.Cookies.TryGetValue("RefreshToken", out string? cookieValue);
                if (cookie)
                {
                    token = (cookieValue != null) ? cookieValue : "";
                }
                var getResult = await _accountRepository.CurrentUser(token);
                if (getResult.isSuccess)
                {
                    result.isSuccess = true;
                    result.StatusCode = 200;
                    result.Message = "";
                    result.Database = getResult;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get user error:" + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 502;
                result.Message = "";
            }
            return result;
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult<dynamic> Logout()
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                if (HttpContext.Request.Cookies.TryGetValue("RefreshToken", out string? cookieValue))
                {
                    Response.Cookies.Delete("RefreshToken");
                    result.isSuccess = true;
                    result.StatusCode = 200;
                    result.Message = "Logout successfully";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Logout error:" + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 502;
                result.Message = "";
            }
            return result;
        }

        [HttpPost]
        [Route("update/{id}")]
        public async Task<ActionResult<dynamic>> Update([FromBody] UpdateAccount req, int id)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var getResult = await _accountRepository.UpdateUser(req, id);
                if (getResult.isSuccess)
                {
                    result.isSuccess = true;
                    result.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Logout error:" + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 502;
                result.Message = "";
            }
            return result;
        }

        [HttpGet]
        [Route("group")]
        public async Task<dynamic> GetUsersByGroupCd([FromHeader] int groupCd)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var getResponse = await _accountRepository.GetUsersByGroup(groupCd);
                if (getResponse.isSuccess)
                {
                    result.isSuccess = true;
                    result.StatusCode = 200;
                    result.Message = "";
                    result.Database = getResponse.Database;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Logout error:" + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 502;
                result.Message = "";
            }
            return result;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("bye/{id}")]
        public async Task<dynamic> DeleteByCd(int id)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var getResponse = await _accountRepository.DeleteUser(id);
                if (getResponse.isSuccess)
                {
                    result.isSuccess = true;
                    result.StatusCode = 200;
                    result.Message = "";
                    result.Database = getResponse.Database;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Logout error:" + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 502;
                result.Message = "";
            }
            return result;
        }
    }
}