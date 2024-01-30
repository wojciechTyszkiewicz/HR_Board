namespace HR_Board.Models.DTO
{
    public class AuthResponseDTO
    {
        public UserDTO User { get; set; }
        public required string Token { get; set; }
    }
}
