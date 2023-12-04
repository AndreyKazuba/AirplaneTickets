using AirplaneTickets.Data.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace AirplaneTickets.Data
{
    public class AirplaneTicketsDbContext : DbContext
    {
        public AirplaneTicketsDbContext() { }

        public AirplaneTicketsDbContext(DbContextOptions<AirplaneTicketsDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=AirplaneTickets;Trusted_Connection=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
