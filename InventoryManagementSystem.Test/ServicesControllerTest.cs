using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test
{
    public class ServicesControllerTest
    {
        Mock<IMapper> mapper;
        Mock<IUnitOfWork> unitOfWork;
        ServicesController controller;
        Service service;
        ServiceDTO serviceDto;
        public ServicesControllerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            controller = new ServicesController(unitOfWork.Object, mapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            service = new Service(){Id = 1, Description = "snips 25g", Code = "0001", Price = 0.5};
            serviceDto = new ServiceDTO(){Id = 1, Description = "snips 25g", Code = "0001", Price = 0.5};
        }

        [Fact]
        public void TestGet_ResultIsOkObject_ValueIsListOfServicesDTO()
        {
            mapper.Setup(x => 
                x.Map<IEnumerable<ServiceDTO>>(It.IsAny<IEnumerable<Service>>())).Returns(new List<ServiceDTO>());

            unitOfWork.Setup(
                x => x.ServiceRepository.Get( It.IsAny<Expression<Func<Service, bool>>>(),
                It.IsAny<Func<IQueryable<Service>, IOrderedQueryable<Service>>>(),
                It.IsAny<string>())).Returns(new List<Service>());

            var actionResult = controller.Get();

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ServiceDTO>>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestGetById_ReturnOkObject_ValueIsServiceDTOIfIdNotNullAndExist()
        {
            mapper.Setup(x => x.Map<ServiceDTO>(It.IsAny<Service>())).Returns(new ServiceDTO());
            unitOfWork.Setup(x => x.ServiceRepository.Get(
                It.IsAny<object>())).Returns(new Service());

            var actionResult = controller.Get(id: 1);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<ServiceDTO>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestGetById_ReturnNotFoundIfIdIsNullOrIdNotExist()
        {
            unitOfWork.Setup(x => x.ServiceRepository.Get(
                It.IsAny<object>())).Returns(null as Service);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        } 

        [Fact]
        public void TestPost_ReturnOkObject_ValueIsServiceIdIfModelStateIsValidAndServiceNotNull()
        {
            mapper.Setup(x => x.Map<Service>(It.IsAny<ServiceDTO>())).Returns(service);
            unitOfWork.Setup(x => x.ServiceRepository.Insert(
                It.IsAny<Service>())).Returns(true);

            var actionResult = controller.Post(serviceDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(serviceDto.Id, ((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPost_ReturnBadRequestIfModelStateNotValidOrServiceIsNull()
        {
            unitOfWork.Setup(x => x.ServiceRepository.Insert(
                It.IsAny<Service>())).Returns(true);

            var actionResult = controller.Post(null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnOkObject_ValueIsServiceIdIfModelStateValidAndIdNotNullAndServiceNotNull()
        {
            mapper.Setup(x => x.Map<Service>(It.IsAny<ServiceDTO>())).Returns(service);
            unitOfWork.Setup(x => x.ServiceRepository.Get(It.IsAny<object>())).Returns(service);
            unitOfWork.Setup(x => x.ServiceRepository.Update(It.IsAny<Service>())).Returns(true);

            var actionResult = controller.Put(serviceDto.Id, serviceDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(serviceDto.Id,((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfModelStateNotValidOrIdIsNullOrServiceIsNull()
        {
            unitOfWork.Setup(x => x.ServiceRepository.Get(It.IsAny<object>())).Returns(service);
            unitOfWork.Setup(x => x.ServiceRepository.Update(It.IsAny<Service>())).Returns(true);

            var actionResult = controller.Put(null, null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        } 

        [Fact]
        public void TestDelete_ReturnOkObject_ValueIsServiceIdIfIdNotNullAndExist()
        {
            unitOfWork.Setup(x => x.ServiceRepository.Get(It.IsAny<object>())).Returns(service);
            unitOfWork.Setup(x => x.ServiceRepository.Delete(It.IsAny<Service>())).Returns(true);

            var actionResult = controller.Delete(service.Id);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(service.Id,((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestDelete_ReturnNotFoundIfIdIsNullOrNotExist()
        {
            unitOfWork.Setup(x => x.ServiceRepository.Get(It.IsAny<object>())).Returns(service);
            unitOfWork.Setup(x => x.ServiceRepository.Delete(It.IsAny<Service>())).Returns(true);

            var actionResult = controller.Delete(null);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestFind_ResultIsOkObject_ValueIsListOfServicesDTO()
        {
            mapper.Setup(x => 
                x.Map<IEnumerable<ServiceDTO>>(It.IsAny<IEnumerable<Service>>())).Returns(new List<ServiceDTO>());

            unitOfWork.Setup(
                x => x.ServiceRepository.Get( It.IsAny<Expression<Func<Service, bool>>>(),
                It.IsAny<Func<IQueryable<Service>, IOrderedQueryable<Service>>>(),
                It.IsAny<string>())).Returns(new List<Service>());

            var actionResult = controller.Find(serviceDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<ServiceDTO>>(((OkObjectResult)actionResult.Result).Value);
        }
    }
}