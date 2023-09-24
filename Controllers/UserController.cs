using BrowseClimate.Models;
using BrowseClimate.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BrowseClimate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        private readonly IConfiguration _config;

        public UserController()
        {
            _userService = new UserService(_config);
        }


        [HttpGet]
        [Route("MyPage")]
        public string MyPage(int id)
        {
            return "Profile page";
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(User user)
        {
            try {

                _userService.CreateUser(user);
                return Ok("User succesfully created " +user);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                User user = await _userService.GetUser(id);
                return Ok(user);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(User user)
        {
            try
            {
                await _userService.UpdateUser(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet]
        [Route("GetPseudo")]
        public async Task<IActionResult> FindUserWithPseudo(string pseudo)
        {
            try {

                User user = await _userService.FindUserWithPseudo(pseudo);
                return Ok(user); 


            }catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


        [HttpDelete]
        [Route("Delete")]
        public async Task <IActionResult> Delete(User user)
        {
            try
            {
                await _userService.DeleteUser(user);
                return Ok(user); 
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(string pseudo, string password)
        {
            // CREATE LOGIN IN USER SERVICE 


            try{ 
                string jwt = await _userService.LoginUser(pseudo, password);
                if (!String.IsNullOrEmpty(jwt))
                {
                    return Ok(jwt);
                }else return NotFound("User not found ");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet]
        [Route("testerror")]
        public async Task<IActionResult> TestError()
        {

            _userService.TestError();
            return BadRequest("check");
        }



        

    }
}
