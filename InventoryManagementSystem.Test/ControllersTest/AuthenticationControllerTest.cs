using AutoMapper;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.User;
using InventoryManagementSystem.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ControllersTest
{
    public class AuthenticationControllerTest
    {
        Mock<IMapper> mapper;
        Mock<IUnitOfWork> unitOfWork;
        Mock<IAuthService> authService;
        AuthenticationController controller;

        public AuthenticationControllerTest(){
            unitOfWork = new Mock<IUnitOfWork>();
            authService = new Mock<IAuthService>();
            mapper = new Mock<IMapper>();
            controller = new AuthenticationController(authService.Object, mapper.Object);
        }

        [Fact]
        public void TestAuthenticate_ReturnBadRequestIfUserIsNull()
        {
            var actionResult = controller.Authenticate(null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestAuthenticate_ReturnUnAuthorizedIfTokenIsNull(){
            mapper.Setup(x => x.Map<User>(It.IsAny<UserCredentialsDTO>())).Returns(new User());
            authService.Setup(x => x.Authenticate(It.IsAny<IUserCredentials>())).Returns(null as string);

            var actionResult = controller.Authenticate(new UserCredentialsDTO());

            Assert.IsType<UnauthorizedObjectResult>(actionResult.Result);
        }

        [Fact]
        public void TestAuthenticate_ReturnOkObject_ValueIsJWTStringIfTokenIsNotNull(){
            mapper.Setup(x => x.Map<User>(It.IsAny<UserCredentialsDTO>())).Returns(new User());
            authService.Setup(x => x.Authenticate(It.IsAny<IUserCredentials>())).Returns("");

            var actionResult = controller.Authenticate(new UserCredentialsDTO());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<string>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestRegister_ReturnBadRequestIfUserIsNull()
        {
            var actionResult = controller.Register(null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestRegister_ReturnUnAuthorizedIfTokenIsNull(){
            mapper.Setup(x => x.Map<User>(It.IsAny<UserDTO>())).Returns(new User());
            authService.Setup(x => x.Register(It.IsAny<User>())).Returns(null as string);

            var actionResult = controller.Register(new UserDTO());

            Assert.IsType<UnauthorizedResult>(actionResult.Result);
        }

        [Fact]
        public void TestRegister_ReturnOkObject_ValueIsJWTStringIfTokenNotNull(){
            mapper.Setup(x => x.Map<User>(It.IsAny<UserDTO>())).Returns(new User());
            authService.Setup(x => x.Register(It.IsAny<User>())).Returns("");

            var actionResult = controller.Register(new UserDTO());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<string>(((OkObjectResult)actionResult.Result).Value);
        }
    }
}