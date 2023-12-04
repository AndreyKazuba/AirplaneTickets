#nullable disable
namespace AirplaneTickets.Models
{
    public class BuyTicketDto
    {
        public Guid TicketId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
