using BrowseClimate.Models;

namespace BrowseClimate.Repositories.CityRepositories
{
    public interface ICityRepository
    {
         Task CreateCity(City city);

         Task<City> GetCity(int id);

         Task UpdateCity(City city);

         Task DeleteCity(int id);

        Task<List<City>> GetAllCities();
    }
}
