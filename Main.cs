using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BrowseClimate
{
    public class Main
    {


        public JObject ReadJSON()
        {
            JObject data = JObject.Parse(File.ReadAllText("‪C:\\Users\\33651\\Desktop\\Sites\\BrowseClimate\\data\\cityProperties.Json"));
            Debug.Write(data);
            return data;

        }

    }
}
