using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HR_Board.Services.Users
{
    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<IdentityError> Errors { get; set; }

        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }


    }
}
