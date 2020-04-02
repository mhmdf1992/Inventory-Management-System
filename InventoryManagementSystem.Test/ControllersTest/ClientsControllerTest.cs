using System.Collections.Generic;
using AutoMapper;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Client;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ControllersTest
{
    public class ClientsControllerTest
    {
        Mock<IMapper> mapper;
        Mock<IUnitOfWork> unitOfWork;
        Mock<EntityService<Client>> entityService;
        ClientsController controller;
        List<Client> list;
        List<ClientDTO> dtoList;
        PagedList<Client> pagedList;
        public ClientsControllerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            entityService = new Mock<EntityService<Client>>(unitOfWork.Object);
            mapper = new Mock<IMapper>();
            controller = new ClientsController(entityService.Object, mapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            list = new List<Client>(){
                new Client() {Id = 1, Name = "Marco Verati", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new Client() {Id = 2, Name = "Benjamin Stone", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new Client() {Id = 3, Name = "Bernardo Silva", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"}
            };
            dtoList = new List<ClientDTO>(){
                new ClientDTO() {id = 1, name = "Marco Verati", telephone = "961 3 822 106", location = "Green fields - wall street 101 GF"},
                new ClientDTO() {id = 2, name = "Benjamin Stone", telephone = "961 3 822 106", location = "Green fields - wall street 101 GF"},
                new ClientDTO() {id = 3, name = "Bernardo Silva", telephone = "961 3 822 106", location = "Green fields - wall street 101 GF"}
            };
            pagedList = new PagedList<Client>(list);
        }

        [Fact]
        public void TestGet_ReturOkObject_ValueIsListOfClientsDTO_PaginationHeaderTotalEqualsListCount()
        {
            entityService.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(pagedList.Set(l => l.Total = pagedList.Count));
            mapper.Setup(x => 
                x.Map<IEnumerable<ClientDTO>>(It.IsAny<IEnumerable<Client>>()))
                    .Returns(dtoList);

            var actionResult = controller.Get(It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ClientDTO>>(((OkObjectResult)actionResult.Result).Value);
            Assert.Equal("{\"total\":" + pagedList.Total + "}", 
                controller.Response.Headers["X-Pagination"]);
        }

        [Fact]
        public void TestFindMatch_ReturnBadRequestIfClientIsNull(){
            var actionResult = controller.FindMatch(txt: null, It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestFindMatch_ReturnOkObject_ValueIsListOfClientsDTO_PaginationHeaderTotalEqualsListCount()
        {
            entityService.Setup(x => x.FindMatch(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(pagedList.Set(l => l.Total = pagedList.Count));
            mapper.Setup(x => 
                x.Map<IEnumerable<ClientDTO>>(It.IsAny<IEnumerable<Client>>()))
                    .Returns(dtoList);

            var actionResult = controller.FindMatch("marco", It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ClientDTO>>(((OkObjectResult)actionResult.Result).Value);
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
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Client);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestGetById_ReturnOkObject_ValueIsClientDtoIfIdNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            mapper.Setup(x => x.Map<ClientDTO>(It.IsAny<Client>())).Returns(dtoList[0]);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<ClientDTO>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPost_ReturnBadRequestIfClientDtoIsNull(){
            var actionResult = controller.Post(null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPost_ReturnOkIfClientDtoNotNull(){
            entityService.Setup(x => x.Insert(It.IsAny<Client>())).Returns(entityService.Object);
            mapper.Setup(x => x.Map<Client>(It.IsAny<ClientDTO>())).Returns(list[0]);

            var actionResult = controller.Post(dtoList[0]);

            Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfIdIsNull(){
            var actionResult = controller.Put(null, dtoList[0]);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfClientDtoIsNull(){
            var actionResult = controller.Put(1, null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnNotFoundIfIdNotExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Client);

            var actionResult = controller.Put(1, dtoList[0]);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnOkIfIdNotNullAndClientDtoNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            entityService.Setup(x => x.Update(It.IsAny<Client>(), It.IsAny<Client>())).Returns(entityService.Object);
            mapper.Setup(x => x.Map<Client>(It.IsAny<ClientDTO>())).Returns(list[0]);

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
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Client);

            var actionResult = controller.Delete(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestDelete_ReturnOkIfIdNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            entityService.Setup(x => x.Delete(It.IsAny<Client>())).Returns(entityService.Object);

            var actionResult = controller.Delete(id: 1);

            Assert.IsType<OkResult>(actionResult.Result);
        }
    }
}