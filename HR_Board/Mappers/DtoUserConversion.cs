using HR_Board.Data;
using HR_Board.Data.ModelDTO;
using HR_Board.Services.Users;
using System.ComponentModel.DataAnnotations;

namespace HR_Board.Mappers
{

    public static class DtoUserConversion
    {
        public static Profile BuildProfile(RegistrationRequestDTO registrationRequestDTO)
        {
            return new Profile
            {
                FirstName = registrationRequestDTO.FirstName,
                LastName = registrationRequestDTO.LastName
            };
        }


        public static RegistrationResponseDTO From(RegistrationResult registrationResponse)
        {
            return new RegistrationResponseDTO()
            {
                Id = registrationResponse.Id,
                CreatedAt = registrationResponse.CreatedAt,
                UpdatedAt = registrationResponse.UpdatedAt,
                FirstName = registrationResponse.FirstName,
                LastName = registrationResponse.LastName
            };
        }


        public static AuthResponseDTO From(AuthResult result)

        {
            return new AuthResponseDTO
            {
                User = new UserDto
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

