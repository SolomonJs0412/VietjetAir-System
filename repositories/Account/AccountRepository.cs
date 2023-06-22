using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.configs;
using flightdocs_system.models.Account;
using flightdocs_system.models.http.http_request.Account;
using flightdocs_system.models.http.http_response;
using flightdocs_system.services.AccountServices;
using flightdocs_system.staticObject.StaticResultResponse;

namespace flightdocs_system.repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration? _config;
        private readonly AccountHelper _accountService;

        public AccountRepository(IConfiguration configuration, AccountHelper accountService)
        {
            _config = configuration;
            _accountService = accountService;
        }

        public async Task<dynamic> CreateNewAccount(CreateTemplate request)
        {
            var result = new CreateAccountResult();
            try
            {
                AccountInfo newAccount = new AccountInfo();
                newAccount.Name = request.Name;
                newAccount.Email = request.Email;
                newAccount.PhoneNumber = request.PhoneNumber;
                newAccount.Role = (request.Role != null) ? request.Role : "GO";
                var passwordSeikyu = _accountService.CreatePasswordHash(request.Password);
                newAccount.PasswordHash = passwordSeikyu["passwordHash"];
                newAccount.PasswordSalt = passwordSeikyu["passwordSalt"];
                newAccount.GroupCd = request.GroupCd;
                newAccount.isActivate = request.isActivate;
                result.isSuccess = await _accountService.SaveDataSources(newAccount);
                if (result.isSuccess)
                {
                    result.Database = newAccount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating from CreateNewAccount" + ex.Message);
                return result;
            }
            return result;
        }

        public async Task<dynamic> Login(LoginTemplate request)
        {
            ServiceResponse result = new ServiceResponse();

            string domain = "vietjetair.com";
            string domainEmail = "@" + domain;

            bool IsDomain = request.Email.EndsWith(domainEmail);
            if (IsDomain == false)
            {
                result.StatusCode = 401;
                result.isSuccess = false;
                result.Message = "Not an domain email address";
                return result;
            }

            var user = _accountService.FindUserByEmail(request.Email);
            if (user.isSuccess == false)
            {
                result.StatusCode = 404;
                result.isSuccess = false;
                result.Message = "Incorrect email address";
                return result;
            }
            else
            {
                AccountInfo account = user.Database;
                var isPasswordCorrect = _accountService.VerifyPassword(request.Password, account.PasswordHash, account.PasswordSalt);
                if (isPasswordCorrect == false)
                {
                    result.StatusCode = 400;
                    result.isSuccess = false;
                    result.Message = "Password email address";
                    return result;
                }

                string token = _accountService.CreatedToken(account.Name, account.Role);

                var restoken = _accountService.RefreshTokenGenerator();
                var refreshToken = _accountService.RefreshTokenSv(account, restoken);

                CookieOptions cookie = new CookieOptions();
                string refresh = "";
                if (refreshToken.Database.TryGetValue("cookie", out object value1))
                {
                    cookie = value1 as CookieOptions;
                }

                refresh = (string)restoken.Token;

                Dictionary<string, object> database = new Dictionary<string, object>();
                database.Add("token", token);
                database.Add("cookieOption", cookie);
                database.Add("refresh-token", refresh);

                result.isSuccess = true;
                result.StatusCode = 200;
                result.Message = "Success";
                result.Database = database;
            }

            return result;
        }

        public async Task<dynamic> CurrentUser(string token)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var getResult = _accountService.GetCurrentUser(token);
                if (getResult.GetType() == typeof(AccountInfo))
                {
                    result.Database = getResult;
                    result.isSuccess = true;
                    result.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating from CreateNewAccount" + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 502;
                return result;
            }
            return result;
        }
        public async Task<ServiceResponse> GetUsersByGroup(int cd)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var getResult = _accountService.UsersByGroup(cd);
                if (getResult != null)
                {
                    result.Database = getResult;
                    result.isSuccess = true;
                    result.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating from CreateNewAccount" + ex.Message);
                result.isSuccess = false;
                result.StatusCode = 502;
                return result;
            }
            return result;
        }

        public async Task<ServiceResponse> UpdateUser(UpdateAccount req, int id)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var getResult = _accountService.UpdateAccount(req, id);

                if (getResult != null)
                {
                    result.isSuccess = true;
                    result.StatusCode = 200;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.StatusCode = 502;
                Console.WriteLine("Error updating user" + ex.Message);
            }
            return result;
        }
        public async Task<ServiceResponse> DeleteUser(int id)
        {
            ServiceResponse result = new ServiceResponse();
            try
            {
                var getResult = _accountService.UpdateAccount(id);

                if (getResult != null)
                {
                    result.isSuccess = true;
                    result.StatusCode = 200;
                }
                return result;
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.StatusCode = 502;
                Console.WriteLine("Error updating user" + ex.Message);
            }
            return result;
        }
    }
}