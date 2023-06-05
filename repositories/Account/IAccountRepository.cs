using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Account;
using flightdocs_system.staticObject.StaticResultResponse;

namespace flightdocs_system.repositories.Account
{
    public interface IAccountRepository
    {
        Task<dynamic> CreateNewAccount(CreateTemplate request);
        Task<dynamic> Login(LoginTemplate request);
        Task<dynamic> CurrentUser(string token);
        Task<ServiceResponse> GetUsersByGroup(int cd);
    }
}