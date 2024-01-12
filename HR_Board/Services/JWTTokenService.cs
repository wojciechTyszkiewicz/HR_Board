using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace HR_Board.Services
{
    public class JWTTokenService
    {
        private readonly IConfiguration _configuration;

        public JWTTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string GenerateJwtToken()
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,  
                audience: null, 
                expires: DateTime.Now.AddDays(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
