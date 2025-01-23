﻿namespace AlertManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string PasswordHash { get; set; } = String.Empty;

        public ICollection<UserPreference> UserPreferences { get; set; } = new List<UserPreference>();
    }
}
