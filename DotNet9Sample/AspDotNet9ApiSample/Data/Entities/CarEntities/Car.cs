using AspDotNet9ApiSample.Data.Entities.CommonEntities;
using AspDotNet9ApiSample.Data.Entities.DealerEntities;

namespace AspDotNet9ApiSample.Data.Entities.CarEntities
{
    public class Car : BaseAuditableEntity
    {
        public string? VIN { get; set; }
        public required string Make { get; set; }
        public required string Model { get; set; }
        public short Year { get; set; }
        public string? StockNumber { get; set; }
        public string? Trim { get; set; }
        public decimal? Price { get; set; }
        public decimal? Mileage { get; set; }
        public bool? IsKM { get; set; }
        public string? ExteriorColor { get; set; }
        public string? Description { get; set; }
        public string? TransmissionType { get; set; }
        public string? FuelType { get; set; }
        public string? BodyType { get; set; }
        public string? Drivetrain { get; set; }
        public string? Condition { get; set; }
        public decimal? EngineSize { get; set; }
        public int? EngineCylinder { get; set; }
        public string? Engine { get; set; }
        public string? InstalledOptions { get; set; }
        public string? InteriorColor { get; set; }
        public int? NumberDoors { get; set; }
        public int? NumberSeats { get; set; }
        public string? MainImage { get; set; }
        public string? Images { get; set; }
        public string? MainImageBackup { get; set; }
        public string? ImagesBackup { get; set; }
        public bool IsCertified { get; set; }
        public bool IsAddedManually { get; set; }
        public bool IsEditedManually { get; set; }
        public string? OriginalBodyType { get; set; }
        public string? OriginalFuelType { get; set; }
        public string? OriginalDrivetrain { get; set; }
        public long DealerReferenceId { get; set; }
        public Dealer DealerReference { get; set; }
    }
}
