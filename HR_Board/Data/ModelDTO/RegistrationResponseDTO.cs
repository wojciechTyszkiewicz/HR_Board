using System.ComponentModel.DataAnnotations;

namespace HR_Board.Data.ModelDTO
{
    public class RegistrationResponseDTO
    {
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
