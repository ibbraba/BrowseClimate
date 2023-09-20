using BrowseClimate.Models;

namespace BrowseClimate.Services.UserServices
{
    public interface IUserService
    {
        Task<User> GetUser(int id);
        Task CreateUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(User user);

        void ValidateUser(User user);

        string EncryptUserPassword(string password);

        Task<User> FindUserWithPseudo(string pseudo);

        void ValidatePassword(User user);

        Task<string> LoginUser(string login, string password);

        string CreateToken(User user);


    }
}
