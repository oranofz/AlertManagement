namespace AlertManagement.Models
{
    public class UserPreference
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string Origin { get; set; } = String.Empty;
        public string Destination { get; set; } = String.Empty;
        public User? User { get; set; }
    }

}
