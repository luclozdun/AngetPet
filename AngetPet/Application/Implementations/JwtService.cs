using AngetPet.Application.Services;
using AngetPet.Domain.Models;
using AngetPet.Infraestructure.Authenticate;
using AngetPet.Shared.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngetPet.Application.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions options;

        public JwtService(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }

        public string GenerateToken(User user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.SecretKey);
            
            var claims = new Claim[]
            {
                new Claim(ConstantHelper.ClaimType.Name, user.Name),
                new Claim(ConstantHelper.ClaimType.UserId, user.Id.ToString()),
                new Claim(ConstantHelper.ClaimType.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Issuer = options.Issuer;
            tokenDescriptor.Subject = new ClaimsIdentity(claims);
            tokenDescriptor.Expires = DateTime.Now.AddDays(5);
            tokenDescriptor.Audience = options.Audience;
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
