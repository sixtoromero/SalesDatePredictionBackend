using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalesDatePrediction.Application.DTO;
using SalesDatePrediction.Application.Interface;
using SalesDatePrediction.Transversal.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesDatePrediction.Services.WebAPIRest.Helpers
{
    public class BuildToken
    {

        public BuildToken() { }


        public string GetBuildToken(UsersDTO UsersDTO,
                                  AppSettings appSettings)
        {
            var keyBytes = Encoding.ASCII.GetBytes(appSettings.Secret);
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim("UserName", UsersDTO.UserName!));
            claims.AddClaim(new Claim("Id", UsersDTO.Id.ToString()));
            claims.AddClaim(new Claim("Email", UsersDTO.Email!));            
            claims.AddClaim(new Claim("FirstName", UsersDTO.FirstName!));
            claims.AddClaim(new Claim("LastName", UsersDTO.LastName!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(appSettings.Expires)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string token = tokenHandler.WriteToken(tokenConfig);

            return token;

        }

        public string GetBuildTokenV1(UsersDTO UsersDTO,
                                  AppSettings appSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,"1")
                }),

                //Expires = DateTime.UtcNow.AddMinutes(1),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = appSettings.IsSuer,
                Audience = appSettings.Audience

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
