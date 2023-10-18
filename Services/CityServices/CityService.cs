using BrowseClimate.Helpers;
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


        public async Task<string> GetLocalTime(City city)
        {
            TimeZoneAPIHelper timeZoneAPIHelper = new TimeZoneAPIHelper();
            string localTime = await timeZoneAPIHelper.RequestLocalTimeZone(city);
            return localTime;
        }


        public async Task<double> GetWheather(City city)
        {
         
            double wheather = await OpenWheatherAPIHelper.GetWheather(city);
            return wheather;
        }


        public async Task CreateCity(City city)
        {

            ValidateCity(city);
            city.CreatedAt = DateTime.Now;
            await _cityRepository.CreateCity(city);
        }

        public async Task DeleteCity(int id)
        {
            await _cityRepository.DeleteCity(id);
        }

        public async Task<List<City>> GetAllCities()
        {
            List<City> cities = await _cityRepository.GetAllCities();
            foreach(City city in cities)
            {
                city.TimeZone = await GetLocalTime(city);
            }

            //TODO Add Timezone

            cities = cities.OrderBy(x => x.CreatedAt).ToList();
            cities = cities.AsEnumerable().Reverse().ToList();
            return cities;

        }

        public async Task<City> GetCity(int id)
        {
         
            
            City city = await _cityRepository.GetCity(id);

            city.TimeZone = await GetLocalTime(city);
            try
            {
                double wheather = await GetWheather(city);
            
                city.Temperature = Convert.ToInt32(wheather);

                
            }
            catch
            {

            }

            
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
