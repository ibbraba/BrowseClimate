using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace BrowseClimate.Helpers
{
    public static class DBHelper
    {

        //  CONNECTION STRING 

        private static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BrowseClimate;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


        //  METHOD TO CONNECT TO DB
        public static IDbConnection connectToDB(string dbname)
        {
           
            IDbConnection connection = new SqlConnection(connectionString);

            return connection;

        }

    }
}
