namespace api.Model
{
    public class Business {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? BusinessName { get; set; }
        public string? SicCode { get; set; }
    }
}