using Microsoft.EntityFrameworkCore;

namespace AlertManagement.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<FlightAlert> FlightAlerts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserPreference>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPreferences)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserPreference>()
                .Property(up => up.Id)
                .ValueGeneratedOnAdd();
        }
    }
}