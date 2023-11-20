using BrowseClimate.Models;
using BrowseClimate.Repositories.FactRepository;

namespace BrowseClimate.Services.FactServices
{
    public class FactService : IFactService
    {
        private IFactRepository _factRepository;

        public FactService()
        {

            _factRepository = new FactRepository();
        }

        public FactService(IFactRepository factRepository) {

            _factRepository = factRepository;
        }

        public async Task AddLike(int factId, int userId)
        {
           await _factRepository.AddLike(factId, userId);
        }

        public async Task CreateFact(Fact fact)
        {
            fact.CreatedAt = DateTime.Now;
            await _factRepository.CreateFact(fact);
        }

        public async Task DeleteFact(int id)
        {
            await _factRepository.DeleteFact(id);   

        }

        public async Task DeleteLike(int factId, int userId)
        {
            await _factRepository.DeleteLike(factId,userId);
        }

        public async Task<List<Fact>> GetAll()
        {
            List<Fact> facts = await _factRepository.GetAll();
            return facts; 
        }

        public async Task<List<Fact>> GetCityFacts(int id)
        {
            List<Fact> facts = await _factRepository.GetCityFacts(id);
            return facts;
        }

        public async Task UpdateFact(Fact fact)
        {
            await _factRepository.UpdateFact(fact);
        }

        public async Task<List<int>> GetUserLikes(int userId)
        {
            List<int> factIds = await _factRepository.GetUserLikes(userId);
            return factIds;
        }
    }
}
