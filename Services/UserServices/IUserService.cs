using BrowseClimate.Models;

namespace BrowseClimate.Services.UserServices
{
    public interface IUserService
    {
        void GetUser(User user);
        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);

        void VerifiyUser(User user);

        void EncryptUserPassword(User user);

        void LoginUser(string pseudo, string password);

        void ValidatePassword(User user);




    }
}
