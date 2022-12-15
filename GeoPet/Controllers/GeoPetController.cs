using Microsoft.AspNetCore.Mvc;
using GeoPet.Services;
using GeoPet.Data;

namespace GeoPet.Controllers
{

    #region Support

    [ApiController]
    [Route("api/[controller]")]
    public class GeoPetController : ControllerBase
    {
        public readonly IGeoPetService _service;
       
        public GeoPetController(IGeoPetService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("localization")]
        public async Task<IActionResult> FindGeoPet(string latitude, string longitude)
        {
            var result = await _service.FindGeoPet(latitude, longitude);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }

    #endregion
}