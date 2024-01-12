using System.ComponentModel.DataAnnotations;

namespace HR_Board.Models
{
    public class RegistrationRequest
    {
        [Required]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
