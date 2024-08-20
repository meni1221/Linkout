using Linkout.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Linkout.Services
{
    public class JtWService
    {
        private IConfiguration config;
        public JtWService(IConfiguration _config) { config = _config; }

        public string genJWToken(UserModel user)
        { 
            string? key = config.GetValue("JWT :key", string.Empty);
            int? exp = config.GetValue("JWT :exp", 3);

            SymmetricSecurityKey secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            SigningCredentials crd = new SigningCredentials(secKey,SecurityAlgorithms.HmacSha256);

            Claim[] claims = new[]
            {
                new Claim("id",user.id.ToString()),
                new Claim("username", user.username)
            };

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes((double)exp),
                signingCredentials: crd,
                claims: claims
                );

            string tkn = new JwtSecurityTokenHandler().WriteToken(token);



            return tkn;
        }
    }
}