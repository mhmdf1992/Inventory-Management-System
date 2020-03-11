using System;
using InventoryManagementSystem.Api.Models.User;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Api.Services.Auth;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ServicesTest
{
    public class JWTAuthServiceTest
    {
        Mock<IUserService> entityService;
        IAuthService authService;
        IJWTService jWTService;
        User user;
        readonly string TOKEN_KEY = "this is my test key";
        public JWTAuthServiceTest(){
            entityService = new Mock<IUserService>();
            authService = new JWTAuthService(entityService.Object, TOKEN_KEY, DateTime.UtcNow.AddHours(1));
            jWTService = new JWTAuthService(entityService.Object, TOKEN_KEY, DateTime.UtcNow.AddHours(1));
            user = new User {Id = 1, Username = "mhmdfayad1992@gmail.com", Password = "9613822106"};
        }

        [Fact]
        public void TestGenerateToken_ReturnStringToken()
        {
            var token = jWTService.GenerateToken(user);

            Assert.IsType<string>(token);
        }

        [Fact]
        public void TestAuthenticate_ReturnNullIfUserNotExist()
        {
            entityService.Setup(x => x.Find(It.IsAny<User>())).Returns(null as User);

            var token = authService.Authenticate(new User());

            Assert.Null(token);
        }

        [Fact]
        public void TestAuthenticate_ReturnStringIfUserExist()
        {
            entityService.Setup(x => x.Find(It.IsAny<User>())).Returns(user);

            var token = authService.Authenticate(new User());

            Assert.IsType<string>(token);
        }
    }
}