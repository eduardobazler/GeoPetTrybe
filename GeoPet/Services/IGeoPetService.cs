using GeoPet.Data;

namespace GeoPet.Services
{
    public interface IGeoPetService
    {
        Task<Localization> FindGeoPet(string latitude, string longitude);
    }
}