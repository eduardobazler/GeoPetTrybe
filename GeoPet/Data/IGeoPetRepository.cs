using GeoPet.Controllers.TypesReq;
using GeoPet.Models;
using System.Drawing;
using GeoPet.Services;
using SkiaSharp;

namespace GeoPet.Data
{
    public interface IGeoPetRepository
    {
        User GetUserById(int userId);
        IEnumerable<User> GetUsers();
        Task<User> CreateUser(User user);
        Pet GetPetById(int petId, int userId);
        IEnumerable<Pet> GetPets(int userId);
        Task<Pet> CreatePet(Pet pet);
        void DeleteUser(User user);
        void AddPetsToUser(Pet Pets, User user);
        Task<GeoLocalization> AddGeoLocalPetsAsync(int userId, int PetId, string lat, string lon);
        Qrcode GenerateQrCode(int PetId);
        byte[] GenerateQrCodeImage(int PetId);
        void DeletePet(Pet pet);
        User FindUser(AuthUser user);
    }
}
