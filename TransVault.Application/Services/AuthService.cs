using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TransVault.Application.Services
{
    public class AuthService : IAuthService
    {
        public List<Claim> generateClaim(string email, string password,string userId)
        {

            var claims = new List<Claim>
            {
                new Claim("username",email),
                new Claim("password",password),
                new Claim("userid",userId)
            };

            return claims;

        }
        public string GeneratejwtSecurityToken(IEnumerable<Claim> claims)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("KEY"));

            var creds = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256Signature);

            var tockenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tockenDescriptor);
            string jwtSecurityTokenHand = tokenHandler.WriteToken(token);

            return jwtSecurityTokenHand;
        }
    }
}
