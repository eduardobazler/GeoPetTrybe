using System.Text.Json.Serialization;

namespace GeoPet.Services.UserService;

public class ViaCep
{
    [JsonPropertyName("cep")]
    public string Cep { get; set; }
}