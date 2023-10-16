using BrowseClimate.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using static System.Net.WebRequestMethods;

namespace BrowseClimate.Helpers
{
    public static class OpenWheatherAPIHelper
    {

        public  static string BASE_URL = "https://api.openweathermap.org/data/2.5/weather?q=";

        public static string API_KEY; 

        public static async Task<double> GetWheather(City city)
        {

         
            string cityName = city.Name.Trim();
            cityName = cityName.Replace(" ", "_");  

            string url = BASE_URL + cityName + "&appid=" + API_KEY + "&units=metric";

            HttpClient httpClient = new HttpClient();
            var req = await httpClient.GetAsync(url);

            var res = await req.Content.ReadAsStringAsync();
            JObject response = JObject.Parse(res);

            return (double)response["main"]["temp"];



        }


    }
}
