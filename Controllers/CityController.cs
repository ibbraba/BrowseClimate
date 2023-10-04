using BrowseClimate.Models;
using BrowseClimate.Services.CityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrowseClimate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private CityService _cityService;
    
        public CityController ()
        {
            _cityService = new CityService();
           

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                City city = await _cityService.GetCity(id);
                return Ok(city);


            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
                
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllCities()
        {
            try
            {
                List<City> cities = await _cityService.GetAllCities();
                return Ok(cities);
                    
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> Create (City city){

            try
            {
                await _cityService.CreateCity(city);
                return Ok(city.Name + " created with success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update(City city)
        {
            try
            {
                await _cityService.UpdateCity(city);
                return Ok(city);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }


        [HttpPost]
        [Route("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(City city)
        {
            try
            {
                await _cityService.DeleteCity(city);
                return Ok(city.Name + " deleted with success !");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }



    }
}
