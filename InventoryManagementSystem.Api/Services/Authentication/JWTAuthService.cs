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
        
        public string Authenticate(IUserCredentials userCred){
            var result = this.userService.Authenticate(userCred);
            return result == null ? null : GenerateToken(result);
        }
        
        public string Register(User user){
            var result = this.userService.Register(user).Save();
            return result == 0 ? null : GenerateToken(user);
        }

        public string GenerateToken(User user){
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim("fname", user.Firstname),
                    new Claim("lname", user.Lastname)
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