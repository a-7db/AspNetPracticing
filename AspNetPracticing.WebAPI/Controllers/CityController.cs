using AspNetPracticing.WebAPI.DTOs;
using AspNetPracticing.WebAPI.Models;
using AspNetPracticing.WebAPI.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetPracticing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService) => _cityService = cityService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return Ok(await _cityService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> PostCity(CityDTO model)
        {
            if (model == null)
                return BadRequest();

            var response = await _cityService.Create(model);

            return NoContent();
        }
    }
}
