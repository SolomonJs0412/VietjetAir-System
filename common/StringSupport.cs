using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flightdocs_system.models.http.http_request.Permission;
using flightdocs_system.models.Permissions;
using Newtonsoft.Json;

namespace flightdocs_system.common
{
    public class StringSupport
    {
        private List<string> ConvertJsonToStringList(string json)
        {
            if (json == "")
            {

            }
            List<string> stringList = JsonConvert.DeserializeObject<List<string>>(json);
            return stringList;
        }

        public string ConvertStringToJson(List<string> users)
        {
            string json = JsonConvert.SerializeObject(users);
            return json;
        }

        public static string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 16)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public dynamic ConvertObjectToJson(List<PerCreateRequest> request)
        {
            dynamic result = "";
            try
            {
                result = JsonConvert.SerializeObject(request);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error when concerning" + exception.Message);
            }
            return result;
        }
    }
}