using BrowseClimate.Models;
using BrowseClimate.Repositories.UserRepositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BrowseClimate.Services.UserServices
{
    public class UserService : IUserService
    {
        private IConfiguration _configuration;
        private IUserRepository _userRepository;



        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
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

        public async Task<string> LoginUser(string login, string password)
        {
           
             User user = await FindUserWithPseudo(login);

            if (user != null)
            {
                if (user.Password.Trim() == password)
                {
                     string jwt = CreateToken(user);
                    return jwt;
                }
                else return null;
            } else return null;
        }

        public void TestError()
        {
            throw new Exception("test error in req");
            
            

        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Pseudo.Trim()), 
                new Claim(ClaimTypes.Role, user.Role.Trim())
            };

            var configToken = "Whatever you want as long as it is goood";



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configToken));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(5), 
                signingCredentials: cred
                ); ;

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }
    }
}
