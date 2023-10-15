using Azure;
using BrowseClimate.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BrowseClimate.Helpers
{
    public class TimeZoneAPIHelper
    {
        public string BASE_URL = "https://timeapi.io/api/Time/current/zone?timeZone=";


        public async Task<string> RequestLocalTimeZone(City city)
        {

            //string timeZone = city.Timezone; 

            string url = BASE_URL + city.TimeZone.Trim();

            //REQUEST TO URL 

            HttpClient httpClient = new HttpClient();
            var req = await httpClient.GetAsync(url);

            var res = await req.Content.ReadAsStringAsync();
            JObject response = JObject.Parse(res);
            string time = response["time"].ToString();

           
            // RETURN TIMEZONE
            return time;

        }


    }
}
