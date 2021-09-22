using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace JWT_Authorization
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        //For not creating database
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        { {"test1","Password1"},{"test2","Password2"}};
        
        private readonly string key;
        public JWTAuthenticationManager(string key)
        {
            this.key = key;
        }
       public string AUthenticate(string username, string password)
        {
            if (!users.Any(u=> u.Key == username && u.Value==password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
