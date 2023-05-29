using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flightdocs_system.services.AccountServices
{
    public class EmailValidatorService
    {
        public Boolean IsDomainEmail(string email)
        {
            try
            {
                string domain = "vietjetair.com";
                string domainEmail = "@" + domain;

                bool isValid = email.EndsWith(domainEmail);
                return isValid;
            }
            catch (FormatException)
            {
                // Invalid email format
                return false;
            }
        }
    }
}