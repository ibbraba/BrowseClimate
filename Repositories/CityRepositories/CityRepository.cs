using BrowseClimate.Helpers;
using BrowseClimate.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Data;
using System.Globalization;

namespace BrowseClimate.Repositories.CityRepositories
{
    public class CityRepository : ICityRepository
    {
       

        public async Task CreateCity(City city)
        {
            

            string name = city.Name; 
            string country = city.Country;
            int numberResidents = city.NumberResidents;
            DateTime createdAt = city.CreatedAt;


            using (System.Data.IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpCity_CreateCity", new
                {
                    name,
                    country,
                    numberResidents, 
                    createdAt
                }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task DeleteCity(int id)
        {
            using (System.Data.IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpCity_DeleteCity", new { id }, commandType: CommandType.StoredProcedure);     
            }
        }

        public async Task<List<City>> GetAllCities()
        {
            using (System.Data.IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QueryAsync<City>("dbo.SpCity_GetAllCities", commandType: CommandType.StoredProcedure);
                return output.ToList();
            }
        }

        public async Task<City> GetCity(int id)
        {
            using (System.Data.IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QuerySingleOrDefaultAsync<City>("dbo.SpCity_FindCityWithId", new { id }, commandType: CommandType.StoredProcedure);
                return output;
            }
        }

        public async Task UpdateCity(City city)
        {

            int id = city.Id;
            string name = city.Name;
            string country = city.Country;
            int numberResidents = city.NumberResidents;



            using (System.Data.IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpCity_EditCity", new { id, name, country, numberResidents }, commandType: CommandType.StoredProcedure);
    
            }


        }



        public async Task AddNote(int cityId, int userId, int note)
        {

            

            using (System.Data.IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpCityNote_AddNote", new { cityId, userId, note}, commandType: CommandType.StoredProcedure);

            }


        }

        public async Task UpdateNote(int cityId, int userId, int note)
        {

            using (System.Data.IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpCityNote_EditNote", new { cityId, userId, note }, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task<int> GetUserNote(int cityId, int userId)
        {

            using (System.Data.IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QuerySingleOrDefaultAsync<int>("dbo.SpCityNote_GetUserNote", new { cityId, userId}, commandType: CommandType.StoredProcedure);
                return output;

            }
        }

        public async Task<List<int>> GetCityNotes(int cityId)
        {
            using (System.Data.IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QueryAsync<int>("dbo.SpCity_GetCityNotes", new { cityId }, commandType: CommandType.StoredProcedure);
                return output.ToList();

            }
        }
    }
}
