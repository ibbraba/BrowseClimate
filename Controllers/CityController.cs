using BrowseClimate.Models;
using BrowseClimate.Services.CityServices;
using BrowseClimate.Services.FactServices;
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
        private FactService _factService;

        public CityController ()
        {
            _cityService = new CityService();
            _factService = new FactService(); 

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                City city = await _cityService.GetCity(id);
                city.Facts = await _factService.GetCityFacts(id);
                
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cityService.DeleteCity(id);
                return Ok("Deleted with success !");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }




    }
}
