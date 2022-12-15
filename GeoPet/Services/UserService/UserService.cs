using System.Net;
using GeoPet.Controllers.TypesReq;
using GeoPet.Data;
using GeoPet.Models;

namespace GeoPet.Services.UserService;

public class UserService : IUserService
{
    private readonly IGeoPetRepository _repository;
    private HttpClient _client;
    private readonly string BaseURL = "https://viacep.com.br/ws";
    private readonly string FormatRequest = "json";

    public UserService(IGeoPetRepository repository, HttpClient client)
    {
        _repository = repository;
        _client = client;
    }

    public async Task<User> CreateUser(ReqUser request)
    {
        var validateCEP = await ValidateCEP(request.Cep);

        if (!validateCEP) throw new Exception("Cep Inválido");
        
        var user = new User()
        {
            Name = request.Name, 
            Cep = request.Cep.ToString(), 
            Email = request.Email, 
            Password = request.Password,
            Created = DateTime.Now
        };
        var createdUser = await _repository.CreateUser(user);
        return createdUser;
    }

    public User FindUser(AuthUser request)
    {
        var user = _repository.FindUser(request);
        return user;
    }


    private async Task<bool> ValidateCEP(int cep)
    {
        var uri = new Uri($"{BaseURL}/{cep}/{FormatRequest}");
        var response = await _client.GetAsync(uri);

        if (response.StatusCode == HttpStatusCode.BadRequest) return false;
        
        var viaCep = await response.Content.ReadFromJsonAsync<ViaCep>();
        
        if (viaCep.Cep is null) return false;

        return true;
    }
}