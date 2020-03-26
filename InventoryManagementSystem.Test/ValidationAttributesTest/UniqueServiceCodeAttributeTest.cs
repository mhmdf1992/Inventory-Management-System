using InventoryManagementSystem.Api.DTOs.ValidationAttributes;
using Xunit;
using Moq;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product;

namespace InventoryManagementSystem.Test.ValidationAttributesTest
{
    public class UniqueServiceCodeAttributeTest
    {
        UniqueServiceCodeAttribute attribute;
        Mock<IProductService<Service>> serviceService;
        ServiceDTO serviceDto = new ServiceDTO(){id = 1, code = "010203"};
        public UniqueServiceCodeAttributeTest(){
            attribute = new UniqueServiceCodeAttribute();
            serviceService = new Mock<IProductService<Service>>();
        }

        [Fact]
        public void TestIsValid_ReturnTrueIfCodeNotExist(){
            serviceService.Setup(x => x.Exist(It.IsAny<string>())).Returns(false);

            var result = attribute.IsValid(serviceDto.code, serviceService.Object, serviceDto);

            Assert.True(result);
        }

        [Fact]
        public void TestIsValid_ReturnTrueIfCodeExistAndServiceDtoIdEqualsServiceId(){
            serviceService.Setup(x => x.Exist(It.IsAny<string>())).Returns(true);
            serviceService.Setup(x => x.Get(It.IsAny<string>())).Returns(new Service(){Id = serviceDto.id, Code = serviceDto.code});

            var result = attribute.IsValid(serviceDto.code, serviceService.Object, serviceDto);

            Assert.True(result);
        }

        [Fact]
        public void TestIsValid_ReturnFalseIfCodeExistAndServiceDtoIdNotEqualsServiceId(){
            serviceService.Setup(x => x.Exist(It.IsAny<string>())).Returns(true);
            serviceService.Setup(x => x.Get(It.IsAny<string>())).Returns(new Service(){Id = 1000, Code = serviceDto.code});

            var result = attribute.IsValid(serviceDto.code, serviceService.Object, serviceDto);

            Assert.False(result);
        }
    }
}