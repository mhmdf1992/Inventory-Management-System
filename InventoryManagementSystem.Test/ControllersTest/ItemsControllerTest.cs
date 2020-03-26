using System.Collections.Generic;
using AutoMapper;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ControllersTest
{
    public class ItemsControllerTest
    {
        Mock<IMapper> mapper;
        Mock<IUnitOfWork> unitOfWork;
        Mock<EntityService<Item>> entityService;
        ItemsController controller;
        List<Item> list;
        List<ItemDTO> dtoList;
        PagedList<Item> pagedList;
        public ItemsControllerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            entityService = new Mock<EntityService<Item>>(unitOfWork.Object);
            mapper = new Mock<IMapper>();
            controller = new ItemsController(entityService.Object, mapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            list = new List<Item>(){
                new Item(){Id = 1, Description = "snips 25g", Code = "0001", Price = 0.5},
                new Item(){Id = 2, Description = "snips 50g", Code = "0002", Price = 1},
                new Item(){Id = 3, Description = "snips 100g", Code = "0003", Price = 1.5},
            };
            dtoList = new List<ItemDTO>(){
                new ItemDTO(){id = 1, description = "snips 25g", code = "0001", price = 0.5},
                new ItemDTO(){id = 2, description = "snips 50g", code = "0002", price = 1},
                new ItemDTO(){id = 3, description = "snips 100g", code = "0003", price = 1.5},
            };
            pagedList = new PagedList<Item>(list);
        }

        [Fact]
        public void TestGet_ReturOkObject_ValueIsListOfItemsDTO_PaginationHeaderTotalEqualsListCount()
        {
            entityService.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(pagedList.Set(l => l.Total = pagedList.Count));
            mapper.Setup(x => 
                x.Map<IEnumerable<ItemDTO>>(It.IsAny<IEnumerable<Item>>()))
                    .Returns(dtoList);

            var actionResult = controller.Get(It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ItemDTO>>(((OkObjectResult)actionResult.Result).Value);
            Assert.Equal("{\"total\":" + pagedList.Total + "}", 
                controller.Response.Headers["X-Pagination"]);
        }

        [Fact]
        public void TestFindMatch_ReturnBadRequestIfItemIsNull(){
            var actionResult = controller.FindMatch(item: null, It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestFindMatch_ReturOkObject_ValueIsListOfItemsDTO_PaginationHeaderTotalEqualsListCount()
        {
            entityService.Setup(x => x.FindMatch(It.IsAny<Item>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(pagedList.Set(l => l.Total = pagedList.Count));
            mapper.Setup(x => 
                x.Map<IEnumerable<ItemDTO>>(It.IsAny<IEnumerable<Item>>()))
                    .Returns(dtoList);

            var actionResult = controller.FindMatch(dtoList[0], It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ItemDTO>>(((OkObjectResult)actionResult.Result).Value);
            Assert.Equal("{\"total\":" + pagedList.Total + "}", 
                controller.Response.Headers["X-Pagination"]);
        }

        [Fact]
        public void TestGetById_ReturnBadRequestIfIdIsNull(){
            var actionResult = controller.Get(id: null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestGetById_ReturnNotFoundIfIdNotExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Item);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestGetById_ReturnOkObject_ValueIsItemDtoIfIdNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            mapper.Setup(x => x.Map<ItemDTO>(It.IsAny<Item>())).Returns(dtoList[0]);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<ItemDTO>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPost_ReturnBadRequestIfItemDtoIsNull(){
            var actionResult = controller.Post(null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPost_ReturnOkIfItemDtoNotNull(){
            entityService.Setup(x => x.Insert(It.IsAny<Item>())).Returns(entityService.Object);
            mapper.Setup(x => x.Map<Item>(It.IsAny<ItemDTO>())).Returns(list[0]);

            var actionResult = controller.Post(dtoList[0]);

            Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfIdIsNull(){
            var actionResult = controller.Put(null, dtoList[0]);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfItemDtoIsNull(){
            var actionResult = controller.Put(1, null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnNotFoundIfIdNotExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Item);

            var actionResult = controller.Put(1, dtoList[0]);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnOkIfIdNotNullAndItemDtoNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            entityService.Setup(x => x.Update(It.IsAny<Item>(), It.IsAny<Item>())).Returns(entityService.Object);
            mapper.Setup(x => x.Map<Item>(It.IsAny<ItemDTO>())).Returns(list[0]);

            var actionResult = controller.Put(1, dtoList[0]);

            Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public void TestDelete_ReturnBadRequestIfIdIsNull(){
            var actionResult = controller.Delete(id: null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestDelete_ReturnNotFoundIfIdNotExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Item);

            var actionResult = controller.Delete(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestDelete_ReturnOkIfIdNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            entityService.Setup(x => x.Delete(It.IsAny<Item>())).Returns(entityService.Object);

            var actionResult = controller.Delete(id: 1);

            Assert.IsType<OkResult>(actionResult.Result);
        }
    }
}