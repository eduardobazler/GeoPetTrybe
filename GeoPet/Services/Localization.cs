using System.Text.Json.Serialization;

namespace GeoPet.Services
{
	public class Localization
	{
		[JsonPropertyName("osm_id")]
		public int osmId { get; set; }
        [JsonPropertyName("display_name")]
        public string displayName { get; set; }
    }
}

