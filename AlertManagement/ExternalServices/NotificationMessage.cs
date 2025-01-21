namespace AlertManagement.ExternalServices
{
    public class NotificationMessage
    {
        public int UserId { get; set; }
        public string FlightDetails { get; set; }
        public decimal Price { get; set; }
        public DateTime AlertTime { get; set; }
    }

}
