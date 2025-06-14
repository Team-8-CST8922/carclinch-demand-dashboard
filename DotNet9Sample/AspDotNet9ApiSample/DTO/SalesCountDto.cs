namespace AspDotNet9ApiSample.DTO
{
    public class SalesCountDto
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string BodyType { get; set; } = null!;
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
