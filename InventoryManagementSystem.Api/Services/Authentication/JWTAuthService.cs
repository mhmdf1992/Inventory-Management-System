using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventoryManagementSystem.Api.Models.User;
using Microsoft.IdentityModel.Tokens;

namespace InventoryManagementSystem.Api.Services.Auth
{
    public class JWTAuthService : IAuthService, IJWTService
    {
        private readonly string tokenKey;
        private readonly DateTime? expires;
        protected readonly IUserService userService;
        public JWTAuthService(IUserService userService, string tokenKey, DateTime? expires){
            this.userService = userService;
            this.tokenKey = tokenKey;
            this.expires = expires;
        }
        
        public string Authenticate(User user){
            var result = userService.Find(user);
            return result == null ? null : GenerateToken(result);
        }

        public string GenerateToken(User user){
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}