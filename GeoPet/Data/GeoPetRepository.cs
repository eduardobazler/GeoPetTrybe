using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GeoPet.Controllers.TypesReq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GeoPet.Services;
using GeoPet.Models;
using QRCoder;

namespace GeoPet.Data
{
    public class GeoPetRepositorys : IGeoPetRepository
    {
        private readonly IGeoPetContext _context;
        private readonly IGeoPetService _service;

        public GeoPetRepositorys(IGeoPetContext context, IGeoPetService service)
        {
            _context = context;
            _service = service;
        }

        public User GetUserById(int userId)
        {
            return _context.User.FirstOrDefault(y => y.UserId == userId);
        }
        public IEnumerable<User> GetUsers()
        {
            return _context.User
                .Select(u => new User
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Pets = u.Pets.Select(p => new Pet
                    {
                        PetId = p.PetId,
                        Name = p.Name,
                        Breed = p.Breed,
                        Size = p.Size,
                        Age = DateTime.Now.Year - p.Age
                    })
                }).ToList();
        }

        public async Task<User> CreateUser(User user)
        {
            var createdUser = await _context.User.AddAsync(user, new CancellationToken(true));
            _context.SaveChanges();
            user.UserId = createdUser.Entity.UserId;
            return user;
        }

        public Pet GetPetById(int petId, int userId)
        {
            return _context.Pet
                .Where(p => p.UserId == userId)
                .Where(p => p.PetId == petId).FirstOrDefault();

        }
        public IEnumerable<Pet> GetPets(int userId)
        {
            return _context.Pet.Where(p => p.UserId == userId).ToList();
        }

        public async Task<Pet> CreatePet(Pet pet)
        {
            var createdPet = await _context.Pet.AddAsync(pet);
            _context.SaveChanges();
            
            return new Pet(){ PetId = createdPet.Entity.PetId};
        }
        public void DeleteUser(User users)
        {
            _context.User.Remove(users);
            _context.SaveChanges();
        }

        public void DeletePet(Pet pet)
        {
            _context.Pet.Remove(pet);
            _context.SaveChanges();
        }


        public void AddPetsToUser(Pet pet, User user)
        {
           var getPet = GetPetById(pet.PetId, user.UserId);
           var getUser = GetUserById(user.UserId);

           if (getPet is null || getUser is null) {
            throw new InvalidOperationException("Este pet ou usuário não existe");
           }

            getUser.UserId = getPet.UserId;
            _context.SaveChanges();

        }

        public async Task<GeoLocalization> AddGeoLocalPetsAsync(int userId, int petId, string latitude, string longitude)
        {

            var getPet = GetPetById(petId, userId);

            if (getPet is null) throw new InvalidOperationException("Este pet não encontrado");

            var result = await _service.FindGeoPet(latitude, longitude);

            if (result is null) throw new InvalidOperationException("Este pet não possui registros");

            var geoPet = new GeoLocalization()
            {
                Localization = result.displayName,
                OsmId = result.osmId,
                FK_PetId = petId,
                Created = DateTime.Now,
            };

            var updatedLocation = await _context.GeoLocalization.AddAsync(geoPet);
            _context.SaveChanges();

            return updatedLocation.Entity;
        }

        public Qrcode GenerateQrCode(int PetId)
        {
            var geoPet = _context.GeoLocalization
                .Where(x => x.FK_PetId == PetId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            var userId = _context.Pet.FirstOrDefault(x => x.PetId == PetId).UserId;

            var getUser = GetUserById(userId);

            var text = geoPet != null
                ? $"Name: {getUser.Name}, E-mail: {getUser.Email}, LastPositionPet: {geoPet.Localization}"
                : $"Name: {getUser.Name}, E-mail: {getUser.Email}, LastPositionPet: Não possui registros";

            var newQrCode = new Qrcode()
            {
                src = $"http://api.qrserver.com/v1/create-qr-code/?data={text}&size=250x250"
            };

            return newQrCode;
        }

        public byte[] GenerateQrCodeImage(int PetId)
        {
            var geoPet = _context.GeoLocalization
                .Where(x => x.FK_PetId == PetId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            var userId = _context.Pet.FirstOrDefault(x => x.PetId == PetId).UserId;

            var getUser = GetUserById(userId);

            var qrcode = new QrCodeImage()
            {
                Name = getUser.Name,
                Email = getUser.Email,
                LastPositionPet = geoPet.Localization != null ? geoPet.Localization : "Não possui registros"
            };

            string json = JsonConvert.SerializeObject(qrcode);

            var Image = GenerateByteArray(json);


            return Image;
        }

        private Bitmap GenerateImage(string JsonQrCode)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(JsonQrCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return qrCodeImage;
        }

        private byte[] GenerateByteArray(string JsonQrCode)
        {
            var image = GenerateImage(JsonQrCode);
            return ImageToByte(image);
        }

        private byte[] ImageToByte(Bitmap img)
        {
            MemoryStream stream = new MemoryStream();
            img.Save(stream, ImageFormat.Png);
            return stream.ToArray();

        }

        public User FindUser(AuthUser user)
        {
            var hasUser = _context.User
                .Where(u => u.Email == user.Email)
                .Where(u => u.Password == user.Password);

            return hasUser.FirstOrDefault();
        }
    }
}
