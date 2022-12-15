using GeoPet.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoPet.Services
{
    public class GeoPetService : IGeoPetService
    {
        private readonly HttpClient _client;
        private const string _url = "https://nominatim.openstreetmap.org/";

        public GeoPetService(HttpClient client) 
        {
            _client = client;
            _client.BaseAddress = new Uri(_url);
        }


        public async Task<Localization> FindGeoPet(string latitude, string longitude)
        {

            _client.DefaultRequestHeaders.Add("User-Agent", "WHATEVER VALUE");

            var _resp = await _client.GetAsync($"reverse?format=json&lat={latitude}&lon={longitude}");

            if(!_resp.IsSuccessStatusCode) return default!;

            var _res = await _resp.Content.ReadFromJsonAsync<Localization>();

            if (_res!.ToString() == "[]") return default!;

            return _res;
        }
    }
}