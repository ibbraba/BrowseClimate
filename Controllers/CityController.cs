using BrowseClimate.Models;
using BrowseClimate.Services.CityServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrowseClimate.Controllers
{
    public class CityController : ControllerBase
    {
        private CityService _cityService;

        public CityController ()
        {
            _cityService = new CityService();
           

        }


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


        public async Task<IActionResult> Create (City city){

            try
            {
                await _cityService.CreateCity(city);
                return Ok(city.Name + " created with success");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


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
