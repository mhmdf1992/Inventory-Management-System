using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test
{
    public class ItemsControllerTest
    {
        Mock<IMapper> mapper;
        Mock<IUnitOfWork> unitOfWork;
        ItemsController controller;
        Item item;
        ItemDTO itemDto;
        public ItemsControllerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            controller = new ItemsController(unitOfWork.Object, mapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            item = new Item(){Id = 1, Description = "snips 25g", Code = "0001", Price = 0.5};
            itemDto = new ItemDTO(){Id = 1, Description = "snips 25g", Code = "0001", Price = 0.5};
        }

        [Fact]
        public void TestGet_ResultIsOkObject_ValueIsListOfItemsDTO()
        {
            mapper.Setup(x => 
                x.Map<IEnumerable<ItemDTO>>(It.IsAny<IEnumerable<Item>>())).Returns(new List<ItemDTO>());

            unitOfWork.Setup(
                x => x.ItemRepository.Get( It.IsAny<Expression<Func<Item, bool>>>(),
                It.IsAny<Func<IQueryable<Item>, IOrderedQueryable<Item>>>(),
                It.IsAny<string>())).Returns(new List<Item>());

            var actionResult = controller.Get();

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ItemDTO>>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestGetById_ReturnOkObject_ValueIsItemDTOIfIdNotNullAndExist()
        {
            mapper.Setup(x => x.Map<ItemDTO>(It.IsAny<Item>())).Returns(new ItemDTO());
            unitOfWork.Setup(x => x.ItemRepository.Get(
                It.IsAny<object>())).Returns(new Item());

            var actionResult = controller.Get(id: 1);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<ItemDTO>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestGetById_ReturnNotFoundIfIdIsNullOrIdNotExist()
        {
            unitOfWork.Setup(x => x.ItemRepository.Get(
                It.IsAny<object>())).Returns(null as Item);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        } 

        [Fact]
        public void TestPost_ReturnOkObject_ValueIsItemIdIfModelStateIsValidAndItemNotNull()
        {
            mapper.Setup(x => x.Map<Item>(It.IsAny<ItemDTO>())).Returns(item);
            unitOfWork.Setup(x => x.ItemRepository.Insert(
                It.IsAny<Item>())).Returns(true);

            var actionResult = controller.Post(itemDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(itemDto.Id, ((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPost_ReturnBadRequestIfModelStateNotValidOrItemIsNull()
        {
            unitOfWork.Setup(x => x.ItemRepository.Insert(
                It.IsAny<Item>())).Returns(true);

            var actionResult = controller.Post(null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnOkObject_ValueIsItemIdIfModelStateValidAndIdNotNullAndItemNotNull()
        {
            mapper.Setup(x => x.Map<Item>(It.IsAny<ItemDTO>())).Returns(item);
            unitOfWork.Setup(x => x.ItemRepository.Get(It.IsAny<object>())).Returns(item);
            unitOfWork.Setup(x => x.ItemRepository.Update(It.IsAny<Item>())).Returns(true);

            var actionResult = controller.Put(itemDto.Id, itemDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(itemDto.Id,((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfModelStateNotValidOrIdIsNullOrItemIsNull()
        {
            unitOfWork.Setup(x => x.ItemRepository.Get(It.IsAny<object>())).Returns(item);
            unitOfWork.Setup(x => x.ItemRepository.Update(It.IsAny<Item>())).Returns(true);

            var actionResult = controller.Put(null, null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        } 

        [Fact]
        public void TestDelete_ReturnOkObject_ValueIsItemIdIfIdNotNullAndExist()
        {
            unitOfWork.Setup(x => x.ItemRepository.Get(It.IsAny<object>())).Returns(item);
            unitOfWork.Setup(x => x.ItemRepository.Delete(It.IsAny<Item>())).Returns(true);

            var actionResult = controller.Delete(item.Id);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(item.Id,((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestDelete_ReturnNotFoundIfIdIsNullOrNotExist()
        {
            unitOfWork.Setup(x => x.ItemRepository.Get(It.IsAny<object>())).Returns(item);
            unitOfWork.Setup(x => x.ItemRepository.Delete(It.IsAny<Item>())).Returns(true);

            var actionResult = controller.Delete(null);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        } 
    }
}