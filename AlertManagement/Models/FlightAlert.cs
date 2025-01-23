namespace AlertManagement.Models
{
    public class FlightAlert
    {
        public int Id { get; set; }
        public string FlightId { get; set; } = String.Empty;
        public string Airline { get; set; } = String.Empty;
        public string Origin { get; set; } = String.Empty;
        public string Destination { get; set; } = String.Empty;
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
