using BrowseClimate.Helpers;
using BrowseClimate.Models;
using BrowseClimate.Repositories.CityRepositories;
using BrowseClimate.Services.FactServices;

namespace BrowseClimate.Services.CityServices
{
    public class CityService : ICityService
    {
        private ICityRepository _cityRepository;
        private FactService _factService;

        public CityService() {

            _cityRepository = new CityRepository();

            _factService = new FactService();    
        
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
                city.Facts = await _factService.GetCityFacts(city.Id);
            }

            //TODO Add Timezone

            cities = cities.OrderBy(x => x.CreatedAt).ToList();
            cities = cities.AsEnumerable().Reverse().ToList();
            return cities;

        }

        public async Task<City> GetCity(int id)
        {
         
            try
            {
                City city = await _cityRepository.GetCity(id);
                city.TimeZone = await GetLocalTime(city);
                double wheather = await GetWheather(city);
                
                if(wheather != double.MinValue)
                {
                city.Temperature = Convert.ToInt32(wheather);

                }

                city.Note = await GetCityNote(id);
                return city;


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            
           
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

        public async Task<int> GetUserNote(int cityId, int userId)
        {
            int note = await _cityRepository.GetUserNote(cityId, userId);
            return note;
        }

        public async Task AddNote(int cityId, int userId, int note)
        {
            await _cityRepository.AddNote(cityId, userId, note);    

        }

        public async Task UpdateNote(int cityId, int userId, int note)
        {
            await _cityRepository.UpdateNote(cityId, userId, note); 
        }

        public async Task<int> GetCityNote(int cityId)
        {
            List<int> cityNotes = await _cityRepository.GetCityNotes(cityId);
            int total = 0;
            foreach (int note in cityNotes)
            {
                total += note;
            }
            if (cityNotes.Count > 0)
            {

                int avg = total / cityNotes.Count;
                return avg;

            }
            else return 0;

        }

        

    }
}
