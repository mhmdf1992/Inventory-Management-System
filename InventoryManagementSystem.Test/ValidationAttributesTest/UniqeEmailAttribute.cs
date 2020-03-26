using InventoryManagementSystem.Api.DTOs.ValidationAttributes;
using Xunit;
using Moq;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Test.ValidationAttributesTest
{
    public class UniqueEmailAttributeTest
    {
        UniqueEmailAttribute attribute;
        Mock<IUserService> userService;
        UserDTO userDto = new UserDTO(){id = 1, email = "mhmdfayad@gmail.com"};
        public UniqueEmailAttributeTest(){
            attribute = new UniqueEmailAttribute();
            userService = new Mock<IUserService>();
        }

        [Fact]
        public void TestIsValid_ReturnTrueIfEmailNotExist(){
            userService.Setup(x => x.Exist(It.IsAny<string>())).Returns(false);

            var result = attribute.IsValid(userDto.email, userService.Object, userDto);

            Assert.True(result);
        }

        [Fact]
        public void TestIsValid_ReturnTrueIfEmailExistAndUserDtoIdEqualsUserId(){
            userService.Setup(x => x.Exist(It.IsAny<string>())).Returns(true);
            userService.Setup(x => x.Get(It.IsAny<string>())).Returns(new User(){Id = userDto.id, Email = userDto.email});

            var result = attribute.IsValid(userDto.email, userService.Object, userDto);

            Assert.True(result);
        }

        [Fact]
        public void TestIsValid_ReturnFalseIfEmailExistAndUserDtoIdNotEqualsUserId(){
            userService.Setup(x => x.Exist(It.IsAny<string>())).Returns(true);
            userService.Setup(x => x.Get(It.IsAny<string>())).Returns(new User(){Id = 1000, Email = userDto.email});

            var result = attribute.IsValid(userDto.email, userService.Object, userDto);

            Assert.False(result);
        }
    }
}