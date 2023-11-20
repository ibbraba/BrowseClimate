using BrowseClimate.Models;
using BrowseClimate.Repositories.UserRepositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
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
            try
            {
                 await ValidateUser(user);
              
                 ValidatePassword(user);
 
                 HashPassword hashPassword = new();

                 string hashedpassword = hashPassword.Generate(user.Password);
 
                 user.Password = hashedpassword;
        
                 user.Role = UserRolesEnum.ROLE_USER;
                 user.CreatedAt = DateTime.Now;
                 await _userRepository.CreateUser(user);


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
                



            //ENCRYPT Password

            

        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task UpdateUser(User user)
        {
            
            await _userRepository.UpdateUser(user); 
        }


        public async Task UpdateFavoriteCity(int cityId, int userId)
        {
            await _userRepository.UpdateFavoriteCity(cityId, userId);
        }


 
        public void ValidatePassword(User user)
        {

            if(user.Password.Length < 6)
            {
                throw new ArgumentException("Veuillez indiquez un mot de passe d'au moins 6 caractères");
            }

     
        }

        public async Task ValidateUser(User user)
        {
            List<User> users = await _userRepository.GetAll();

            foreach (User DBUser in users)
            {
                if (DBUser.Pseudo.Trim() == user.Pseudo) {

                    throw new ArgumentException("Pseudonyme déja utilisé. Veuillez en choisir un autre");

                }


                if (DBUser.Email.Trim() == user.Email)
                {
                    throw new ArgumentException("Cette adresse email est déja associée à un compte");

                }
                
            }

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
            try
            {
                HashPassword hashPassword = new();
                User user = await FindUserWithPseudo(login);

                if (user != null)
                {
                    string DBPassword = user.Password.Trim();

                    bool okPassword = hashPassword.IsValid(password, DBPassword);

                    if (okPassword == true)
                    {
                        string jwt = CreateToken(user);
                        return jwt;
                    }
                    else throw new Exception("Identifiants invalides");
                }
                else throw new Exception("Identifiants invalides");
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.Message);
            }
    
        }

        public void TestError()
        {
            throw new Exception("test error in req");
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("pseudo", user.Pseudo.Trim()), 
                new Claim("role", user.Role.Trim())
            };

            var configToken = "Whatever you want as long as it is goood";



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configToken));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(4), 
                signingCredentials: cred
                ); ;

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }

        public async Task<List<User>> GetAll()
        {
            List<User> users = await _userRepository.GetAll();
            return users;
        }
    }
}
