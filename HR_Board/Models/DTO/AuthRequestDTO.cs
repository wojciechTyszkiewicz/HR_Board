namespace HR_Board.Models.DTO
{
    public class AuthRequestDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}