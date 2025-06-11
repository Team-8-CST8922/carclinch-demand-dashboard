using AspDotNet9ApiSample.Data.Entities.CarEntities;
using AspDotNet9ApiSample.Data.Entities.DealerEntities;
using Microsoft.EntityFrameworkCore;

namespace AspDotNet9ApiSample.Data
{
    public class CarClinchDbContext : DbContext
    {
        public CarClinchDbContext(DbContextOptions<CarClinchDbContext> options)
               : base(options)
        {
        }

        public DbSet<Car> Car { get; set; }
        public DbSet<ArchivedCar> ArchivedCar { get; set; }
        public DbSet<Dealer> Dealer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
