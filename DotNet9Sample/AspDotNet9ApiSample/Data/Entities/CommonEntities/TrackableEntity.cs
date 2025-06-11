namespace Api.Data.Domain.Entities.CommonEntities
{
    public class TrackableEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastUpdatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }

        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
    }
}
