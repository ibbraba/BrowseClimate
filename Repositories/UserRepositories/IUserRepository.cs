using BrowseClimate.Models;

namespace BrowseClimate.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task CreateUser(User user); 

        Task<User> GetUser(int id);

        Task UpdateUser(User user);
        Task DeleteUser(int id);

        Task<User> FindOneWithPseudo(string pseudo);

        Task<List<User>> GetAll();

        Task UpdateFavoriteCity(int cityId, int userId);

    }
}
