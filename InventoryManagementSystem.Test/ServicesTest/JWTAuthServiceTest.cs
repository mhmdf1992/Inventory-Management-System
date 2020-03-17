using System;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Models.User;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Api.Services.Auth;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ServicesTest
{
    public class JWTAuthServiceTest
    {
        Mock<EntityService<User>> entityService;
        Mock<IUserService> userService;
        IAuthService authService;
        IJWTService jWTService;
        User user;
        IUserCredentials userCredentials;
        readonly string TOKEN_KEY = "this is my test key";
        public JWTAuthServiceTest(){
            entityService = new Mock<EntityService<User>>(new Mock<IUnitOfWork>().Object);
            userService = new Mock<IUserService>();
            authService = new JWTAuthService(userService.Object, TOKEN_KEY, DateTime.UtcNow.AddHours(1));
            jWTService = new JWTAuthService(userService.Object, TOKEN_KEY, DateTime.UtcNow.AddHours(1));
            user = new User {Id = 1, Firstname="Mhmd", Lastname="Fayad", Email = "mhmdfayad@gmail.com", Password="admin"};
            userCredentials = new User {Id = 1, Email = "mhmdfayad1992@gmail.com", Password = "admin"};
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
            userService.Setup(x => x.Authenticate(It.IsAny<IUserCredentials>())).Returns(null as User);

            var token = authService.Authenticate(userCredentials);

            Assert.Null(token);
        }

        [Fact]
        public void TestAuthenticate_ReturnStringIfUserExist()
        {
            userService.Setup(x => x.Authenticate(It.IsAny<IUserCredentials>())).Returns(user);

            var token = authService.Authenticate(userCredentials);

            Assert.IsType<string>(token);
        }

        [Fact]
        public void TestRegister_ReturnNullIfUserNotSaved()
        {
            entityService.Setup(x => x.Save()).Returns(0);
            userService.Setup(x => x.Register(It.IsAny<User>())).Returns(entityService.Object);

            var token = authService.Authenticate(userCredentials);

            Assert.Null(token);
        }

        [Fact]
        public void TestRegister_ReturnStringIfUserSaved()
        {
            entityService.Setup(x => x.Save()).Returns(1);
            userService.Setup(x => x.Register(It.IsAny<User>())).Returns(entityService.Object);

            var token = authService.Register(user);

            Assert.IsType<string>(token);
        }
    }
}