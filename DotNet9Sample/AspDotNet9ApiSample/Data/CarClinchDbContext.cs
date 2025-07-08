using AspDotNet9ApiSample.Data.Entities.CarEntities;
using AspDotNet9ApiSample.Data.Entities.DealerEntities;
using AspDotNet9ApiSample.DTO;                // ← add this
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

        // Add DbSets for each DTO
        public DbSet<SalesCountDto>           SalesCounts           { get; set; }
        public DbSet<SearchCountDto>          SearchCounts          { get; set; }
        public DbSet<AverageDaysDto>          AverageDays           { get; set; }
        public DbSet<MakeModelDurationDto>    MakeModelDurations    { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tell EF these types have no key and are only for FromSqlRaw queries
            modelBuilder.Entity<SalesCountDto>().HasNoKey();
            modelBuilder.Entity<SearchCountDto>().HasNoKey();
            modelBuilder.Entity<AverageDaysDto>().HasNoKey();
            modelBuilder.Entity<MakeModelDurationDto>().HasNoKey();
        }
    }
}
