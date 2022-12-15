using Microsoft.AspNetCore.Mvc;

namespace GeoPet.Controllers
{
    public interface IGeoPetController
    {
        Task<IActionResult> FindGeoPet(string latitude, string longitude);
    }
}