using BrowseClimate.Models;

namespace BrowseClimate.Services.FactServices
{
    public interface IFactService
    {
        public Task CreateFact(Fact fact);

        public Task UpdateFact(Fact fact);

        public Task<List<Fact>> GetAll();

        public Task<List<Fact>> GetCityFacts(int id);


        public Task DeleteFact(int id);

        public Task AddLike(int factId, int userId);
        public Task DeleteLike(int factId, int userId);
    }
}
