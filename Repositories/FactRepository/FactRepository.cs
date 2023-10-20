using BrowseClimate.Helpers;
using BrowseClimate.Models;
using Dapper;
using System.Data;

namespace BrowseClimate.Repositories.FactRepository
{
    public class FactRepository : IFactRepository
    {
        public async Task AddLike(int factId, int userId)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpFact_AddLike", new { factId, userId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task CreateFact(Fact fact)
        {
            string title = fact.Title; 
            string description = fact.Description;
            DateTime createdAt = fact.CreatedAt;
            int cityId = fact.CityId;

            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpFact_CreateOne", new { title, description, createdAt, cityId}, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFact(int id)
        {

            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpFact_DeleteOne", new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteLike(int factId, int userId)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpFact_DeleteLike", new { factId, userId}, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Fact>> GetAll()
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QueryAsync<Fact>("dbo.SpFact_GetAll", commandType: CommandType.StoredProcedure);
                output = output.ToList();

                foreach (Fact fact in output)
                {
                    fact.Likes = await GetFactNumberLikes(fact.Id);
                }

                return output.ToList();
            }
        }

        public async Task<List<Fact>> GetCityFacts(int cityId)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QueryAsync<Fact>("dbo.SpFact_GetCityFacts", new { cityId }, commandType: CommandType.StoredProcedure);
            


                foreach (Fact fact in output)
                {
                    fact.Likes = await GetFactNumberLikes(fact.Id);
                }

                return output.ToList();
            }
        }

        public async Task UpdateFact(Fact fact)
        {
            int id = fact.Id;
            string title = fact.Title;
            string description = fact.Description;
            int cityId = fact.CityId;

            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpFact_UpdateOne", new { id, title, description, cityId } ,commandType: CommandType.StoredProcedure);
              
            }
        }

        public async Task<int> GetFactNumberLikes(int factId)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteScalarAsync<int>("dbo.SpFact_GetNumberLikes", new { factId }, commandType: CommandType.StoredProcedure);
                return output;
            }
        }

    }
}
