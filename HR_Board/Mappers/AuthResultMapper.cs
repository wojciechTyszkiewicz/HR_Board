using HR_Board.Models.DTO;
using HR_Board.Services.Users;

namespace HR_Board.Mappers
{
    public static class AuthResultMapper
    {
        public static AuthResponseDTO FromAuthResult (this AuthResult result)
            
        {
            return new AuthResponseDTO
            {
                User = new UserDTO
                {
                    Id = result.User.Id.ToString(),
                    CreatedAt = result.User.CreatedAt.ToString(),
                    UpdatedAt = result.User.UpdatedAt.ToString(),
                    FirstName = result.User.UserName,
                    LastName = result.User.LastName,
                    Email = result.User.Email
                },
                Token = result.Token
            };
        }
    }
}
