using InventoryManagementSystem.Api.DTOs.ValidationAttributes;
using Xunit;
using Moq;
using System.ComponentModel.DataAnnotations;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.DTOs;

namespace InventoryManagementSystem.Test.ValidationAttributesTest
{
    public class UniqueItemCodeAttributeTest
    {
        UniqueItemCodeAttribute attribute;
        Mock<IProductService<Item>> itemService;
        ItemDTO itemDto = new ItemDTO(){id = 1, code = "010203"};
        public UniqueItemCodeAttributeTest(){
            attribute = new UniqueItemCodeAttribute();
            itemService = new Mock<IProductService<Item>>();
        }

        [Fact]
        public void TestIsValid_ReturnTrueIfCodeNotExist(){
            itemService.Setup(x => x.Exist(It.IsAny<string>())).Returns(false);

            var result = attribute.IsValid(itemDto.code, itemService.Object, itemDto);

            Assert.True(result);
        }

        [Fact]
        public void TestIsValid_ReturnTrueIfCodeExistAndItemDtoIdEqualsItemId(){
            itemService.Setup(x => x.Exist(It.IsAny<string>())).Returns(true);
            itemService.Setup(x => x.Get(It.IsAny<string>())).Returns(new Item(){Id = itemDto.id, Code = itemDto.code});

            var result = attribute.IsValid(itemDto.code, itemService.Object, itemDto);

            Assert.True(result);
        }

        [Fact]
        public void TestIsValid_ReturnFalseIfCodeExistAndItemDtoIdNotEqualsItemId(){
            itemService.Setup(x => x.Exist(It.IsAny<string>())).Returns(true);
            itemService.Setup(x => x.Get(It.IsAny<string>())).Returns(new Item(){Id = 1000, Code = itemDto.code});

            var result = attribute.IsValid(itemDto.code, itemService.Object, itemDto);

            Assert.False(result);
        }
    }
}