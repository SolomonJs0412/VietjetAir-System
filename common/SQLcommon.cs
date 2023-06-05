using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace flightdocs_system.common
{
    public class SQLcommon
    {
        // private readonly IConfiguration _config;

        // public SQLcommon(string connectionString, IConfiguration config)
        // {
        //     _config = config;
        // }

        public DataTable ExecuteQuery(string query)
        {
            string connectionStr =
            "Server = localhost,1433; Database = flightdocs; User Id = sa; Password = Ashleynguci@1412; TrustServerCertificate = True; Encrypt = false";

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    DataTable dataTable = new DataTable();
                    SqlDataReader dataReader = command.ExecuteReader();
                    dataTable.Load(dataReader);
                    return dataTable;
                }
            }
        }
    }
}