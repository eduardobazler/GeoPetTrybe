using GeoPet.Controllers.TypesReq;
using GeoPet.Data;
using GeoPet.Models;

namespace GeoPet.Services.PetService;

public class PetService : IPetService
{
    private readonly IGeoPetRepository _repository;

    public PetService(IGeoPetRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Pet> CreatePet(Pet pet)
    {
        var hasUser = _repository.GetUserById(pet.UserId);

        if (hasUser is null) throw new Exception("Usuário não encontrado");
        
        var createdPet = await _repository.CreatePet(pet);
        return createdPet;
    }

    public void DeletePet(int petId, int userId)
    {
        var hasUser = _repository.GetUserById(userId);

        var hasPet = _repository.GetPetById(petId, userId);

        if (hasUser is null) throw new Exception("Usuário não encontrado");
        
        if (hasPet is null) throw new Exception("Pet não encontrado");
        
        _repository.DeletePet(hasPet);
    }
}