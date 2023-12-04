#nullable disable
namespace AirplaneTickets.Models
{
    public class RegisterDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
