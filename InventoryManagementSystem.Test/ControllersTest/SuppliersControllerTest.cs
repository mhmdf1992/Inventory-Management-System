using System.Collections.Generic;
using AutoMapper;
using InventoryManagementSystem.Api.Controllers;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using InventoryManagementSystem.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ControllersTest
{
    public class SuppliersControllerTest
    {
        Mock<IMapper> mapper;
        Mock<IUnitOfWork> unitOfWork;
        Mock<EntityService<Supplier>> entityService;
        SuppliersController controller;
        List<Supplier> list;
        List<SupplierDTO> dtoList;
        PagedList<Supplier> pagedList;
        public SuppliersControllerTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            entityService = new Mock<EntityService<Supplier>>(unitOfWork.Object);
            mapper = new Mock<IMapper>();
            controller = new SuppliersController(entityService.Object, mapper.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            list = new List<Supplier>(){
                new Supplier() {Id = 1, Name = "Marco Verati", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new Supplier() {Id = 2, Name = "Benjamin Stone", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new Supplier() {Id = 3, Name = "Bernardo Silva", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"}
            };
            dtoList = new List<SupplierDTO>(){
                new SupplierDTO() {Id = 1, Name = "Marco Verati", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new SupplierDTO() {Id = 2, Name = "Benjamin Stone", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new SupplierDTO() {Id = 3, Name = "Bernardo Silva", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"}
            };
            pagedList = new PagedList<Supplier>(list);
        }

        [Fact]
        public void TestGet_ReturOkObject_ValueIsListOfSuppliersDTO_PaginationHeaderTotalEqualsListCount()
        {
            entityService.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(pagedList.Set(l => l.Total = pagedList.Count));
            mapper.Setup(x => 
                x.Map<IEnumerable<SupplierDTO>>(It.IsAny<IEnumerable<Supplier>>()))
                    .Returns(dtoList);

            var actionResult = controller.Get(It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<SupplierDTO>>(((OkObjectResult)actionResult.Result).Value);
            Assert.Equal("{\"total\":" + pagedList.Total + "}", 
                controller.Response.Headers["X-Pagination"]);
        }

        [Fact]
        public void TestFindMatch_ReturnBadRequestIfSupplierIsNull(){
            var actionResult = controller.FindMatch(supplier: null, It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestFindMatch_ReturOkObject_ValueIsListOfSuppliersDTO_PaginationHeaderTotalEqualsListCount()
        {
            entityService.Setup(x => x.FindMatch(It.IsAny<Supplier>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(pagedList.Set(l => l.Total = pagedList.Count));
            mapper.Setup(x => 
                x.Map<IEnumerable<SupplierDTO>>(It.IsAny<IEnumerable<Supplier>>()))
                    .Returns(dtoList);

            var actionResult = controller.FindMatch(dtoList[0], It.IsAny<int>(), It.IsAny<int>());

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<List<SupplierDTO>>(((OkObjectResult)actionResult.Result).Value);
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
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Supplier);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestGetById_ReturnOkObject_ValueIsSupplierDtoIfIdNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            mapper.Setup(x => x.Map<SupplierDTO>(It.IsAny<Supplier>())).Returns(dtoList[0]);

            var actionResult = controller.Get(id: 1);

            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<SupplierDTO>(((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public void TestPost_ReturnBadRequestIfSupplierDtoIsNull(){
            var actionResult = controller.Post(null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPost_ReturnOkIfSupplierDtoNotNull(){
            entityService.Setup(x => x.Insert(It.IsAny<Supplier>())).Returns(entityService.Object);
            mapper.Setup(x => x.Map<Supplier>(It.IsAny<SupplierDTO>())).Returns(list[0]);

            var actionResult = controller.Post(dtoList[0]);

            Assert.IsType<OkResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfIdIsNull(){
            var actionResult = controller.Put(null, dtoList[0]);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnBadRequestIfSupplierDtoIsNull(){
            var actionResult = controller.Put(1, null);

            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnNotFoundIfIdNotExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Supplier);

            var actionResult = controller.Put(1, dtoList[0]);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestPut_ReturnOkIfIdNotNullAndSupplierDtoNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            entityService.Setup(x => x.Update(It.IsAny<Supplier>(), It.IsAny<Supplier>())).Returns(entityService.Object);
            mapper.Setup(x => x.Map<Supplier>(It.IsAny<SupplierDTO>())).Returns(list[0]);

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
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(null as Supplier);

            var actionResult = controller.Delete(id: 1);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public void TestDelete_ReturnOkIfIdNotNullAndExist(){
            entityService.Setup(x => x.Get(It.IsAny<long>())).Returns(list[0]);
            entityService.Setup(x => x.Delete(It.IsAny<Supplier>())).Returns(entityService.Object);

            var actionResult = controller.Delete(id: 1);

            Assert.IsType<OkResult>(actionResult.Result);
        }
    }
}