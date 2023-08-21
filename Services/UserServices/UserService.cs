﻿using BrowseClimate.Models;
using BrowseClimate.Repositories.UserRepositories;

namespace BrowseClimate.Services.UserServices
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public UserService(IUserRepository userRepository)
        {

            _userRepository = userRepository;

        }

        public async Task CreateUser(User user)
        {
            ValidateUser(user);
            //ENCRYPT Password
            user.Role = UserRolesEnum.ROLE_USER;
            user.CreatedAt = DateTime.Now;

            await _userRepository.CreateUser(user);
        }

        public async Task DeleteUser(User user)
        {
            await _userRepository.DeleteUser(user.Id);
        }

        public async Task UpdateUser(User user)
        {
            
            await _userRepository.UpdateUser(user); 
        }


     


        public void ValidatePassword(User user)
        {
            throw new NotImplementedException();
        }

        public void ValidateUser(User user)
        {

        }

        public async Task<User> GetUser(int id)
        {
           User user = await _userRepository.GetUser(id);
            return user;
        }

        public string EncryptUserPassword(string password)
        {
            throw new NotImplementedException();
        }



        
        public async Task<User> FindUserWithPseudo(string pseudo)
        {
            User user = await _userRepository.FindOneWithPseudo(pseudo);
            return user;
        }

        public async Task<User> LoginUser(string login, string password)
        {
           
             User user = await FindUserWithPseudo(login);

            if (user != null)
            {
                if (user.Password == password)
                {
                    return user;
                }
                else return null;
            } else return null;
        }

        public void TestError()
        {
            throw new Exception("test error in req");
            
            

        }
    }
}
