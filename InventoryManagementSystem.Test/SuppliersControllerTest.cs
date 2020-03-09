using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test
{
    public class SuppliersControllerTest
    {
        Mock<IMapper> mapper;
        Mock<IUnitOfWork> unitOfWork;
        SuppliersController controller;
        Supplier supplier;
        SupplierDTO supplierDto;
        public SuppliersControllerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            controller = new SuppliersController(unitOfWork.Object, mapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            supplier = new Supplier(){Id = 1, Name = "Shakib al sokari", Location = "Lebanon", Email = "shakib@sokari.lb", Telephone = "9613822106"};
            supplierDto = new SupplierDTO(){Id = 1, Name = "Shakib al sokari", Location = "Lebanon", Email = "shakib@sokari.lb", Telephone = "9613822106"};
        }

        [Fact]
        public void TestGet_ResultIsOkObject_ValueIsListOfSuppliersDTO()
        {
            mapper.Setup(x => 
                x.Map<IEnumerable<SupplierDTO>>(It.IsAny<IEnumerable<Supplier>>())).Returns(new List<SupplierDTO>());

            unitOfWork.Setup(
                x => x.SupplierRepository.Get( It.IsAny<Expression<Func<Supplier, bool>>>(),
                It.IsAny<Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>>>(),
                It.IsAny<string>())).Returns(new List<Supplier>());

            var actionResult = controller.Get();

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<SupplierDTO>>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestGetById_ReturnOkObject_ValueIsSupplierDTOIfIdNotNullAndExist()
        {
            mapper.Setup(x => x.Map<SupplierDTO>(It.IsAny<Supplier>())).Returns(new SupplierDTO());
            unitOfWork.Setup(x => x.SupplierRepository.Get(
                It.IsAny<object>())).Returns(new Supplier());

            var actionResult = controller.Get(id: 1);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<SupplierDTO>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestGetById_ReturnNotFoundIfIdIsNullOrIdNotExist()
        {
            unitOfWork.Setup(x => x.SupplierRepository.Get(
                It.IsAny<object>())).Returns(null as Supplier);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        } 

        [Fact]
        public void TestPost_ReturnOkObject_ValueIsSupplierIdIfModelStateIsValidAndSupplierNotNull()
        {
            mapper.Setup(x => x.Map<Supplier>(It.IsAny<SupplierDTO>())).Returns(supplier);
            unitOfWork.Setup(x => x.SupplierRepository.Insert(
                It.IsAny<Supplier>())).Returns(true);

            var actionResult = controller.Post(supplierDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(supplierDto.Id, ((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPost_ReturnBadRequestIfModelStateNotValidOrSupplierIsNull()
        {
            unitOfWork.Setup(x => x.SupplierRepository.Insert(
                It.IsAny<Supplier>())).Returns(true);

            var actionResult = controller.Post(null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnOkObject_ValueIsSupplierIdIfModelStateValidAndIdNotNullAndSupplierNotNull()
        {
            mapper.Setup(x => x.Map<Supplier>(It.IsAny<SupplierDTO>())).Returns(supplier);
            unitOfWork.Setup(x => x.SupplierRepository.Get(It.IsAny<object>())).Returns(supplier);
            unitOfWork.Setup(x => x.SupplierRepository.Update(It.IsAny<Supplier>())).Returns(true);

            var actionResult = controller.Put(supplierDto.Id, supplierDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(supplierDto.Id,((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfModelStateNotValidOrIdIsNullOrSupplierIsNull()
        {
            unitOfWork.Setup(x => x.SupplierRepository.Get(It.IsAny<object>())).Returns(supplier);
            unitOfWork.Setup(x => x.SupplierRepository.Update(It.IsAny<Supplier>())).Returns(true);

            var actionResult = controller.Put(null, null);

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        } 

        [Fact]
        public void TestDelete_ReturnOkObject_ValueIsSupplierIdIfIdNotNullAndExist()
        {
            unitOfWork.Setup(x => x.SupplierRepository.Get(It.IsAny<object>())).Returns(supplier);
            unitOfWork.Setup(x => x.SupplierRepository.Delete(It.IsAny<Supplier>())).Returns(true);

            var actionResult = controller.Delete(supplier.Id);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(supplier.Id,((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestDelete_ReturnNotFoundIfIdIsNullOrNotExist()
        {
            unitOfWork.Setup(x => x.SupplierRepository.Get(It.IsAny<object>())).Returns(supplier);
            unitOfWork.Setup(x => x.SupplierRepository.Delete(It.IsAny<Supplier>())).Returns(true);

            var actionResult = controller.Delete(null);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestFind_ResultIsOkObject_ValueIsListOfSuppliersDTO()
        {
            mapper.Setup(x => 
                x.Map<IEnumerable<SupplierDTO>>(It.IsAny<IEnumerable<Supplier>>())).Returns(new List<SupplierDTO>());

            unitOfWork.Setup(
                x => x.SupplierRepository.Get( It.IsAny<Expression<Func<Supplier, bool>>>(),
                It.IsAny<Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>>>(),
                It.IsAny<string>())).Returns(new List<Supplier>());

            var actionResult = controller.Find(supplierDto);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<SupplierDTO>>(((OkObjectResult)actionResult.Result).Value);
        }
    }
}