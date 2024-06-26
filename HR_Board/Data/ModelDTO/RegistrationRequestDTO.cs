﻿using System.ComponentModel.DataAnnotations;

namespace HR_Board.Data.ModelDTO
{
    public class RegistrationRequestDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string PasswordRepeat { get; set; }

    }
}
