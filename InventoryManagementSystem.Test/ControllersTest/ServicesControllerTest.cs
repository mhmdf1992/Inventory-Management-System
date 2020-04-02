using System.Collections.Generic;
using AutoMapper;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ControllersTest
{
    public class ServicesControllerTest
    {
        Mock<IMapper> mapper;
        Mock<IUnitOfWork> unitOfWork;
        Mock<EntityService<Service>> entityService;
        ServicesController controller;
        List<Service> list;
        List<ServiceDTO> dtoList;
        PagedList<Service> pagedList;
        public ServicesControllerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            entityService = new Mock<EntityService<Service>>(unitOfWork.Object);
            mapper = new Mock<IMapper>();
            controller = new ServicesController(entityService.Object, mapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            list = new List<Service>(){
                new Service(){Id = 1, Description = "extract container", Code = "s0001", Price = 100},
                new Service(){Id = 2, Description = "install house furniture", Code = "s0002", Price = 150},
                new Service(){Id = 3, Description = "business consultancy", Code = "s0003", Price = 1.5},
            };
            dtoList = new List<ServiceDTO>(){
                new ServiceDTO(){id = 1, description = "extract container", code = "s0001", price = 100},
                new ServiceDTO(){id = 2, description = "install house furniture", code = "s0002", price = 150},
                new ServiceDTO(){id = 3, description = "business consultancy", code = "s0003", price = 1.5},
            };
            pagedList = new PagedList<Service>(list);
        }

        [Fact]
        public void TestGet_ReturOkObject_ValueIsListOfServicesDTO_PaginationHeaderTotalEqualsListCount()
        {
            entityService.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(pagedList.Set(l => l.Total = pagedList.Count));
            mapper.Setup(x => 
                x.Map<IEnumerable<ServiceDTO>>(It.IsAny<IEnumerable<Service>>()))
                    .Returns(dtoList);

            var actionResult = controller.Get(It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ServiceDTO>>(((OkObjectResult)actionResult.Result).Value);
            Assert.Equal("{\"total\":" + pagedList.Total + "}", 
                controller.Response.Headers["X-Pagination"]);
        }

        [Fact]
        public void TestFindMatch_ReturnBadRequestIfServiceIsNull(){
            var actionResult = controller.FindMatch(txt: null, It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestFindMatch_ReturnOkObject_ValueIsListOfServicesDTO_PaginationHeaderTotalEqualsListCount()
        {
            entityService.Setup(x => x.FindMatch(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(pagedList.Set(l => l.Total = pagedList.Count));
            mapper.Setup(x => 
                x.Map<IEnumerable<ServiceDTO>>(It.IsAny<IEnumerable<Service>>()))
                    .Returns(dtoList);

            var actionResult = controller.FindMatch("extract", It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ServiceDTO>>(((OkObjectResult)actionResult.Result).Value);
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
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Service);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestGetById_ReturnOkObject_ValueIsServiceDtoIfIdNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            mapper.Setup(x => x.Map<ServiceDTO>(It.IsAny<Service>())).Returns(dtoList[0]);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<ServiceDTO>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPost_ReturnBadRequestIfServiceDtoIsNull(){
            var actionResult = controller.Post(null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPost_ReturnOkIfServiceDtoNotNull(){
            entityService.Setup(x => x.Insert(It.IsAny<Service>())).Returns(entityService.Object);
            mapper.Setup(x => x.Map<Service>(It.IsAny<ServiceDTO>())).Returns(list[0]);

            var actionResult = controller.Post(dtoList[0]);

            Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfIdIsNull(){
            var actionResult = controller.Put(null, dtoList[0]);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfServiceDtoIsNull(){
            var actionResult = controller.Put(1, null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnNotFoundIfIdNotExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Service);

            var actionResult = controller.Put(1, dtoList[0]);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnOkIfIdNotNullAndServiceDtoNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            entityService.Setup(x => x.Update(It.IsAny<Service>(), It.IsAny<Service>())).Returns(entityService.Object);
            mapper.Setup(x => x.Map<Service>(It.IsAny<ServiceDTO>())).Returns(list[0]);

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
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Service);

            var actionResult = controller.Delete(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestDelete_ReturnOkIfIdNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            entityService.Setup(x => x.Delete(It.IsAny<Service>())).Returns(entityService.Object);

            var actionResult = controller.Delete(id: 1);

            Assert.IsType<OkResult>(actionResult.Result);
        }
    }
}