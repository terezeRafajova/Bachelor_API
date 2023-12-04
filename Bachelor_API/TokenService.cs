using Bachelor_API.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bachelor_API
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateAcessToken(Teacher teacher)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["String Key"]);
            var skey = new SymmetricSecurityKey(key);
            var SignedCredential = new SigningCredentials(skey, SecurityAlgorithms.HmacSha256Signature);
            var uClaims = new ClaimsIdentity(new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub,teacher.Username)
            });
            var expires = DateTime.UtcNow.AddHours(12);     

            //create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = uClaims,
                Expires = expires,
                Issuer = "Bachelor_API",
                SigningCredentials = SignedCredential,
            };

            //create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenJwt = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenJwt);

            return token;
        }
    }
}
