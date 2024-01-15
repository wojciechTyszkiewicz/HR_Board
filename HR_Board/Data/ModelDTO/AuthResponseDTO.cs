namespace HR_Board.Data.ModelDTO
{
    public class AuthResponseDTO
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }

    }
}
