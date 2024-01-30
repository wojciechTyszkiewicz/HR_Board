namespace HR_Board.Data.ModelDTO
{
    public class AuthResponseDTO
    {
        public UserDTO User { get; set; }
        public required string Token { get; set; }
    }
}
