using Microsoft.AspNetCore.Mvc;
using REST_API_Shashin.Classes;
using REST_API_Shashin.Models;

namespace REST_API_Shashin.Controllers
{
    [Route("/dishes")]
    public class DishesController : Controller
    {
        DBManager dbManager = new DBManager();
        [Route("/version")]
        [HttpGet]
        public ActionResult GetVersion()
        {
            try
            {
                var versions = dbManager.Dishes
                    .Where(d => !string.IsNullOrEmpty(d.version))
                    .Select(d => d.version)
                    .Distinct()
                    .OrderBy(v => v)
                    .ToList();


                return Ok(new
                {
                    versions
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        public ActionResult GetDishes([FromQuery] string version)
        {
            try
            {
                if (string.IsNullOrEmpty(version))
                {
                    return BadRequest("Version parameter is required");
                }

                var dishes = dbManager.Dishes
                    .Where(d => d.version == version)
                    .ToList();

                if (dishes == null || !dishes.Any())
                {
                    return NotFound($"No dishes found for version {version}");
                }

                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
