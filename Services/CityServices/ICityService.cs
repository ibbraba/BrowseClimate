using BrowseClimate.Models;

namespace BrowseClimate.Services.CityServices
{
    public interface ICityService
    {
        void CreateCity(City city);
        City GetCity(int id);

        void UpdateCity(City city);
        void DeleteCity(City city);



    }
}
