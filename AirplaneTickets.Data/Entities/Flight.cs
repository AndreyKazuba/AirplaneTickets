using System.ComponentModel.DataAnnotations;

#nullable disable
namespace AirplaneTickets.Data.Entities
{
    public class Flight
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Plane { get; set; }
    }
}
