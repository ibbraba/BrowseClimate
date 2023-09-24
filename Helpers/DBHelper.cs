using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Data;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BrowseClimate.Helpers
{
    public static class DBHelper
    {
        

        public static string _cnn { get; set; }

        //  CONNECTION STRING 



        private static string keyVaultrl = "https://bc-azkw.vault.azure.net/";






        //  METHOD TO CONNECT TO 
        public static IDbConnection connectToDB()
        {
            string cs = Regex.Unescape(_cnn);
          
//            string _connectionString = _configuration.GetValue<string>("KeyVaultURL");
            IDbConnection connection = new SqlConnection(cs);
            return connection;

        }

    }
}
