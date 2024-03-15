namespace HR_Board.Data.ModelDTO
{
    public class AuthResponseDTO
    {
        public UserDto User { get; set; }
        public required string Token { get; set; }
    }
}
