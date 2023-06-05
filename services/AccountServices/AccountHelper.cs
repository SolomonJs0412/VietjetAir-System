using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using flightdocs_system.configs;
using flightdocs_system.models.Account;
using flightdocs_system.staticObject.StaticResultResponse;
using Microsoft.IdentityModel.Tokens;

namespace flightdocs_system.services.AccountServices
{
    public class AccountHelper
    {
        private readonly IConfiguration _config;

        private readonly SystemDbContext _dbContext;

        public AccountHelper(IConfiguration configuration, SystemDbContext? dbContext)
        {
            _config = configuration;
            _dbContext = dbContext;
        }

        public async Task<Boolean> SaveDataSources(AccountInfo account)
        {
            try
            {
                _dbContext.Add(account);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data sources" + ex.Message);
                return false;
            }

        }

        public dynamic GetCurrentUser(string token)
        {
            AccountInfo user = new AccountInfo();
            try
            {
                var result = _dbContext.Accounts.FirstOrDefault(u => u.RefreshToken == token);
                if (result != null)
                {
                    user = result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data sources" + ex.Message);
                return ex.Message;
            }
            return user;

        }
        public dynamic UsersByGroup(int cd)
        {
            dynamic result = 0;
            try
            {
                var getResult = _dbContext.Accounts.Where(g => g.GroupCd == cd).ToList();
                if (result != null)
                {
                    result = getResult;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data sources" + ex.Message);
                return ex.Message;
            }
            return (result != null) ? result : false;
        }

        public string CreatedToken(string name, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            };
            var tokenValue = (_config.GetSection("Token:TokenValue").Value.IsNullOrEmpty()) ?
            _config.GetSection("Token:TokenValue").Value : "abc-egc-24-23he0-323-q232q";

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenValue));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMonths(6),
                signingCredentials: cred
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public dynamic CreatePasswordHash(string password)
        {
            Dictionary<string, byte[]> result = new Dictionary<string, byte[]>();
            using (var hmac = new HMACSHA1())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));


                result.Add("passwordHash", passwordHash);
                result.Add("passwordSalt", passwordSalt);
            }
            return result;
        }

        public dynamic FindUserByEmail(string email)
        {
            ServiceResponse result = new ServiceResponse();
            var user = _dbContext.Accounts.FirstOrDefault(a => a.Email == email);
            if (user == null)
            {
                result.isSuccess = false;
                result.Message = "False to Find";
                return result;
            }
            result.isSuccess = true;
            result.Message = "";
            result.Database = user;

            return result;
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA1(passwordSalt))
            {
                var cmpHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return cmpHash.SequenceEqual(passwordHash);
            }
        }

        public dynamic RefreshTokenSv(AccountInfo user, dynamic token)
        {
            ServiceResponse result = new ServiceResponse();
            var cookie = SetRefreshToken(token, user);
            var cookieOption = cookie.Database;


            Dictionary<string, dynamic> database = new Dictionary<string, object>();
            database.Add("cookie", cookieOption);

            result.isSuccess = true;
            result.Database = database;
            return result;
        }

        public RefreshToken RefreshTokenGenerator()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddMonths(6)
            };

            return refreshToken;
        }

        public dynamic SetRefreshToken(RefreshToken refreshToken, AccountInfo user)
        {
            ServiceResponse result = new ServiceResponse();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Expires = refreshToken.Expires
            };


            user.RefreshToken = refreshToken.Token;
            user.CreatedTime = refreshToken.Created;
            user.ExpiresTime = refreshToken.Expires;

            result.isSuccess = true;
            result.Database = cookieOptions;
            _dbContext.SaveChanges();
            return result;
        }
    }
}