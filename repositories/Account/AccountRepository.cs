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
    }
}