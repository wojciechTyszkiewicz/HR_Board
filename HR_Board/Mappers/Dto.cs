﻿using HR_Board.Data;
using HR_Board.Models.DTO;
using HR_Board.Services.Users;
using System.ComponentModel.DataAnnotations;

namespace HR_Board.Mappers
{

    public static class Dto
    {
        public static Profile BuildProfile(this RegistrationRequestDTO registrationRequestDTO)
        {
            return new Profile
            {
                FirstName = registrationRequestDTO.FirstName,
                LastName = registrationRequestDTO.LastName
            };
        }


        public static RegistrationResponseDTO From(RegistrationResponse registrationResponse)
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

        public static RegistrationResponse CreateRegistrationResponse(ApiUser user, bool success, string message)
        {

            return new RegistrationResponse
            {
                Success = success,
                Message = message,
                Id = user.Id.ToString(),
                CreatedAt = user.CreatedAt.ToString(),
                UpdatedAt = user.UpdatedAt.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

    }
}

