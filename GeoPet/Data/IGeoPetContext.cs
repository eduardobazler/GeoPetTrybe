using Microsoft.EntityFrameworkCore;
using GeoPet.Models;

namespace GeoPet.Data
{
    public interface IGeoPetContext
    {
        public DbSet<GeoLocalization> GeoLocalization { get; set; }
        public DbSet<Pet> Pet { get; set; }
        public DbSet<User> User { get; set; }
        public int SaveChanges();
    }
}

