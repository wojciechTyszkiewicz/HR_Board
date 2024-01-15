namespace HR_Board.ModelDTO
{
    public class AuthResponseDTO
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }

    }
}
