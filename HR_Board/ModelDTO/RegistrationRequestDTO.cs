using System.ComponentModel.DataAnnotations;

namespace HR_Board.ModelDTO
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]  
        public string Password { get; set; }

    }
}
