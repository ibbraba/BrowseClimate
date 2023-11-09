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

        Task AddNote(int cityId, int userId, int note);

        Task UpdateNote(int cityId, int userId, int note);

        Task<int> GetUserNote(int cityId, int userId);

        Task<List<int>> GetCityNotes(int cityId);

        Task<int> GetNumberFans(int cityId);


    }
}
