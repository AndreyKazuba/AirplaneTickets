using System.ComponentModel.DataAnnotations;

#nullable disable
namespace AirplaneTickets.Data.Entities
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid FlightId { get; set; }

        [Required]
        public string Seat { get; set; }

        [Required]
        public int RootPrice { get; set; }

        [Required]
        public bool Sold { get; set; }

        public Guid? OwnerId { get; set; }
        public string PassengerFirstName { get; set; }
        public string PassengerLastName { get; set; }
    }
}
