using System.ComponentModel.DataAnnotations;

#nullable disable
namespace AirplaneTickets.Data.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
