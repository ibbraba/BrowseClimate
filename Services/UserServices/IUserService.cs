using BrowseClimate.Models;

namespace BrowseClimate.Services.UserServices
{
    public interface IUserService
    {
        User GetUser(int id);
        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);

        void VerifiyUser(User user);

        string EncryptUserPassword(string password);

        User FindUserWithPseudo(string pseudo);

        void ValidatePassword(User user);




    }
}
