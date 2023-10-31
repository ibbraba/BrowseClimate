using BrowseClimate.Helpers;
using BrowseClimate.Models;
using Dapper;
using System.Data;

namespace BrowseClimate.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {


        public async Task CreateUser(User user)
        {

            string name = user.Name;
            string firstname = user.FirstName; 
            string email = user.Email;
            string pseudo = user.Pseudo;
            string password = user.Password;
            string role = user.Role;
            int favoriteCity = user.FavoriteCity;
            DateTime CreatedAt = user.CreatedAt;

            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpUsers_CreateUser", new {
                    name, 
                    firstname, 
                    email, 
                    pseudo,
                    password,
                    role,
                    CreatedAt,
                    favoriteCity,
                }, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task DeleteUser(int id)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpUsers_DeleteUser", new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<User> FindOneWithPseudo(string pseudo)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QuerySingleOrDefaultAsync<User>("dbo.SpUsers_FindUserWithPseudo", new { pseudo }, commandType: CommandType.StoredProcedure);
                return output;
            }
        }

        public async Task<List<User>> GetAll()
        {

            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QueryAsync<User>("dbo.SpUser_GetAll",  commandType: CommandType.StoredProcedure);
                return output.ToList();
            }

        }

        public async Task<User> GetUser(int id)
        {
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.QuerySingleOrDefaultAsync<User>("dbo.SpUsers_FindUserWithId", new { id }, commandType: CommandType.StoredProcedure); 
                return output;
            }
        }

        public async Task UpdateFavoriteCity(int cityId, int userId)
        {
            
            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpUsers_UpdateFavoriteCity", new { cityId, userId }, commandType: CommandType.StoredProcedure);
                
            }


        }

        public async Task UpdateUser(User user)
        {
            int id = user.Id;
            string name = user.Name;
            string firstname = user.FirstName;
            string email = user.Email;
            string pseudo = user.Pseudo;
            string password = user.Password;
            int favoriteCity = user.FavoriteCity;
       

            using (IDbConnection db = DBHelper.connectToDB())
            {
                var output = await db.ExecuteAsync("dbo.SpUsers_EditUser", new {
                    id,
                    name,
                    firstname,
                    email,
                    pseudo,
                    password,
                    favoriteCity
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
