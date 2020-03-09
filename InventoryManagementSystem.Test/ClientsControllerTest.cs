using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test
{
    public class ClientsControllerTest
    {
        Mock<IMapper> mapper;
        Mock<IUnitOfWork> unitOfWork;
        ClientsController controller;
        Client client;
        ClientDTO clientDto;
        public ClientsControllerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            controller = new ClientsController(unitOfWork.Object, mapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            client = new Client(){Id = 1, Name = "Shakib al sokari", Location = "Lebanon", Email = "shakib@sokari.lb", Telephone = "9613822106"};
            clientDto = new ClientDTO(){Id = 1, Name = "Shakib al sokari", Location = "Lebanon", Email = "shakib@sokari.lb", Telephone = "9613822106"};
        }

        [Fact]
        public void TestGet_ResultIsOkObject_ValueIsListOfClientsDTO()
        {
            mapper.Setup(x => 
                x.Map<IEnumerable<ClientDTO>>(It.IsAny<IEnumerable<Client>>())).Returns(new List<ClientDTO>());

            unitOfWork.Setup(
                x => x.ClientRepository.Get( It.IsAny<Expression<Func<Client, bool>>>(),
                It.IsAny<Func<IQueryable<Client>, IOrderedQueryable<Client>>>(),
                It.IsAny<string>())).Returns(new List<Client>());

            var actionResult = controller.Get();

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ClientDTO>>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestGetById_ReturnOkObject_ValueIsClientDTOIfIdNotNullAndExist()
        {
            mapper.Setup(x => x.Map<ClientDTO>(It.IsAny<Client>())).Returns(new ClientDTO());
            unitOfWork.Setup(x => x.ClientRepository.Get(
                It.IsAny<object>())).Returns(new Client());

            var actionResult = controller.Get(id: 1);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<ClientDTO>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestGetById_ReturnNotFoundIfIdIsNullOrIdNotExist()
        {
            unitOfWork.Setup(x => x.ClientRepository.Get(
                It.IsAny<object>())).Returns(null as Client);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        } 

        [Fact]
        public void TestPost_ReturnOkObject_ValueIsClientIdIfModelStateIsValidAndClientNotNull()
        {
            mapper.Setup(x => x.Map<Client>(It.IsAny<ClientDTO>())).Returns(client);
            unitOfWork.Setup(x => x.ClientRepository.Insert(
                It.IsAny<Client>())).Returns(true);

            var actionResult = controller.Post(clientDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(clientDto.Id, ((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPost_ReturnBadRequestIfModelStateNotValidOrClientIsNull()
        {
            unitOfWork.Setup(x => x.ClientRepository.Insert(
                It.IsAny<Client>())).Returns(true);

            var actionResult = controller.Post(null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnOkObject_ValueIsClientIdIfModelStateValidAndIdNotNullAndClientNotNull()
        {
            mapper.Setup(x => x.Map<Client>(It.IsAny<ClientDTO>())).Returns(client);
            unitOfWork.Setup(x => x.ClientRepository.Get(It.IsAny<object>())).Returns(client);
            unitOfWork.Setup(x => x.ClientRepository.Update(It.IsAny<Client>())).Returns(true);

            var actionResult = controller.Put(clientDto.Id, clientDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(clientDto.Id,((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfModelStateNotValidOrIdIsNullOrClientIsNull()
        {
            unitOfWork.Setup(x => x.ClientRepository.Get(It.IsAny<object>())).Returns(client);
            unitOfWork.Setup(x => x.ClientRepository.Update(It.IsAny<Client>())).Returns(true);

            var actionResult = controller.Put(null, null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        } 

        [Fact]
        public void TestDelete_ReturnOkObject_ValueIsClientIdIfIdNotNullAndExist()
        {
            unitOfWork.Setup(x => x.ClientRepository.Get(It.IsAny<object>())).Returns(client);
            unitOfWork.Setup(x => x.ClientRepository.Delete(It.IsAny<Client>())).Returns(true);

            var actionResult = controller.Delete(client.Id);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(client.Id,((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestDelete_ReturnNotFoundIfIdIsNullOrNotExist()
        {
            unitOfWork.Setup(x => x.ClientRepository.Get(It.IsAny<object>())).Returns(client);
            unitOfWork.Setup(x => x.ClientRepository.Delete(It.IsAny<Client>())).Returns(true);

            var actionResult = controller.Delete(null);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestFind_ResultIsOkObject_ValueIsListOfClientsDTO()
        {
            mapper.Setup(x => 
                x.Map<IEnumerable<ClientDTO>>(It.IsAny<IEnumerable<Client>>())).Returns(new List<ClientDTO>());

            unitOfWork.Setup(
                x => x.ClientRepository.Get( It.IsAny<Expression<Func<Client, bool>>>(),
                It.IsAny<Func<IQueryable<Client>, IOrderedQueryable<Client>>>(),
                It.IsAny<string>())).Returns(new List<Client>());

            var actionResult = controller.Find(clientDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ClientDTO>>(((OkObjectResult)actionResult.Result).Value);
        }
    }
}