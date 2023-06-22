using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public List<object> SliceString(string input)
        {
            string[] segments = input.Split('-');
            List<object> result = new List<object>();

            foreach (string segment in segments)
            {
                string type = segment.Substring(0, 1);
                string permission = segment.Substring(1, 1);
                string group = segment.Substring(2);

                var obj = new
                {
                    type = type,
                    permission = permission,
                    group = group
                };

                result.Add(obj);
            }

            return result;
        }
    }
}