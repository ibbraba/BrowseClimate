﻿using BrowseClimate.Models;
using BrowseClimate.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public string MyPage(int id)
        {
            return "Profile page";
        }


        [HttpPost]
        [Route("Create")]
   
        public async Task<IActionResult> CreateUser(User user)
        {
            try {

                await _userService.CreateUser(user);
                return Ok("User succesfully created " + user);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        [Route("Get")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public async Task <IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok("User with Id " + id + " deleted."); 
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
        [Route("validate")]
        [Authorize]
        public async Task<IActionResult> VaidateToken()
        {


            return Ok(true);

        }


        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<User> users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }


        [HttpGet]
        [Route("UpdateFavoriteCity")]
           
        public async Task<IActionResult> UpdateFavoriteCity(int cityId, int userId)
        {

            try
            {
                await _userService.UpdateFavoriteCity(cityId, userId);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

    }
}
