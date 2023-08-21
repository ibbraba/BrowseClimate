using BrowseClimate.Models;
using BrowseClimate.Repositories.CityRepositories;

namespace BrowseClimate.Services.CityServices
{
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;

        public CityService() {

            _cityRepository = new CityRepository();
        
        }    

        public CityService(ICityRepository cityRepository) {

            _cityRepository = cityRepository;
        
        }



        public async Task CreateCity(City city)
        {

            ValidateCity(city);
            await _cityRepository.CreateCity(city);
        }

        public async Task DeleteCity(City city)
        {
            await _cityRepository.DeleteCity(city.Id);
        }

        public async Task<List<City>> GetAllCities()
        {
            List<City> cities = await _cityRepository.GetAllCities();
            return cities;

        }

        public async Task<City> GetCity(int id)
        {
            City city = await _cityRepository.GetCity(id);
            return city;
        }

        public async Task UpdateCity(City city)
        {
            ValidateCity(city); 
            await _cityRepository.UpdateCity(city); 
        }

        public void ValidateCity(City city)
        {
            if(String.IsNullOrEmpty(city.Name)  || String.IsNullOrEmpty(city.Description) || String.IsNullOrEmpty(city.Country))
            {
                throw new ArgumentException("Veuillez indiquer le pays, la ville et sa description"); 
            }
        }
    }
}
