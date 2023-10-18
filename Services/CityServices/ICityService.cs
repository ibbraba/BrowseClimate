using BrowseClimate.Models;

namespace BrowseClimate.Services.CityServices
{
    public interface ICityService
    {
        Task CreateCity(City city);
        Task<City> GetCity(int id);

        Task UpdateCity(City city);
        Task DeleteCity(int id);
        void ValidateCity(City city);

        Task<List<City>> GetAllCities();


    }
}
