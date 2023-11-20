using BrowseClimate.Models;
using BrowseClimate.Services.FactServices;
using Microsoft.AspNetCore.Mvc;

namespace BrowseClimate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactController : ControllerBase
    {
        private FactService _factService;

        public FactController()
        {
            _factService = new FactService();
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Fact fact)
        {

            try
            {
                await _factService.CreateFact(fact);
                return Ok(fact);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }


        [HttpGet]
        [Route("UserLikes")]
        public async Task<IActionResult> GetUserLikes(int userId)
        {
            try
            {
                List<int> factIds = await _factService.GetUserLikes(userId); 
                return Ok(factIds);
                

            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int factId)
        {
            try
            {
                _factService.DeleteFact(factId);
                return Ok("Fact deleted with success");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<Fact> facts = await _factService.GetAll();
                foreach(Fact fact in facts)
                {

                    fact.Timestamp = (int)fact.CreatedAt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                }
                return Ok(facts);

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Route("GetCityFacts")]
        public async Task<IActionResult> GetCityFacts(int cityId)
        {
            try
            {
                List<Fact> facts = await _factService.GetCityFacts(cityId);
                return Ok(facts);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }

        }


        [HttpPost]
        [Route("UpdateFact")]
        public async Task<IActionResult> UpdateFact(Fact fact)
        {
            try
            {
                await _factService.UpdateFact(fact);
                return Ok(fact); 
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("AddLikeToFact")]
        public async Task<IActionResult> AddLikeToFact(int factId, int userId)
        {
            try
            {
                await _factService.AddLike(factId, userId);
                return Ok("Like ajouté");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("DeleteLike")]
        public async Task<IActionResult> DeleteLike(int factId, int userId)
        {
            try
            {
                await _factService.DeleteLike(factId, userId);
                return Ok("Like supprimé ! ");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

    }
}
