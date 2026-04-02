using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using REST_API_Shashin.Models;

namespace REST_API_Shashin.Classes
{
    public class JwtToken
    {
        static byte[] Key = Encoding.UTF8.GetBytes("BEBEBEBEBEBEBEBEBEBE");
        public static string Generate(User user)
        {
            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Key),
                    SecurityAlgorithms.HmacSha256Signature
                    )
            };
            SecurityToken Token = TokenHandler.CreateToken(tokenDescriptor);
            return TokenHandler.WriteToken(Token);
        }
    }
}
