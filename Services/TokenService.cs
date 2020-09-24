using ApiTask.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiTask.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings appSettings;

        public TokenService(IOptions<AppSettings> options)
        {
            appSettings = options.Value;
        }

        public string GetToken(LoginUsers loginUsers)
        {
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(loginUsers);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public SecurityTokenDescriptor GetTokenDescriptor(LoginUsers loginUsers)
        {
            const int tokenExpiration = 2;

            byte[] securityKey = Encoding.UTF8.GetBytes(appSettings.EncryptionKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(securityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loginUsers.ApiKey)
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpiration),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenDescriptor;
        }

        
    }
}
