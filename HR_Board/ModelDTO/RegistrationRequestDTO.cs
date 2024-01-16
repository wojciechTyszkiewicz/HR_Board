using System.ComponentModel.DataAnnotations;

namespace HR_Board.ModelDTO
{
    public class RegistrationRequestDTO
    {
        []
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordRepeat { get; set; }

        [Required]  
        public string Password { get; set; }

    }
}
