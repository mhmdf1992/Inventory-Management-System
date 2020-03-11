using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Services;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ServicesTest
{
    public class ServiceServiceTest
    {
        Mock<IUnitOfWork> unitOfWork;
        EntityService<Service> serviceService;
        List<Service> list;
        PagedList<Service> pagedList;
        public ServiceServiceTest(){
            unitOfWork = new Mock<IUnitOfWork>();
            serviceService = new ServiceService(unitOfWork.Object);
            list = new List<Service>(){
                new Service(){Id = 1, Description = "extract container", Code = "s0001", Price = 100},
                new Service(){Id = 2, Description = "install house furniture", Code = "s0002", Price = 150},
                new Service(){Id = 3, Description = "business consultancy", Code = "s0003", Price = 1.5}
            };
            pagedList = new PagedList<Service>(list);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(0, 3)]
        public void TestGet_ReturnPagedListOfService_ListCountEqualsTake_TotalEqualsQueriedListCount(int skip, int take){
            unitOfWork.Setup(
                x => x.ServiceRepository.Get( It.IsAny<Expression<Func<Service, bool>>>(),
                 It.IsAny<Func<IQueryable<Service>, IOrderedQueryable<Service>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = serviceService.Get(skip, take);

            Assert.IsType<PagedList<Service>>(result);
            Assert.Equal(result.Count, take);
            Assert.Equal(result.Total, list.Count);
        }

        [Fact]
        public void TestFindMatch_ReturnPagedListOfService_ListCountEqualsTake_TotalEqualsQueriedListCount(){
            unitOfWork.Setup(
                x => x.ServiceRepository.Get( It.IsAny<Expression<Func<Service, bool>>>(),
                 It.IsAny<Func<IQueryable<Service>, IOrderedQueryable<Service>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = serviceService.FindMatch(list[0], 0, 1);

            Assert.IsType<PagedList<Service>>(result);
            Assert.Equal(result.Count, 1);
            Assert.Equal(result.Total, list.Count);
        }

        [Fact]
        public void TestGet_ReturnService(){
            unitOfWork.Setup(x => x.ServiceRepository.Get(It.IsAny<long>())).Returns(list[0]);
            
            var result = serviceService.Get(1);

            Assert.IsType<Service>(result);
        }

        [Fact]
        public void TestInsert_ReturnServiceService(){
            unitOfWork.Setup(x => x.ServiceRepository.Insert(It.IsAny<Service>())).Returns(true);

            var result = serviceService.Insert(list[0]);

            Assert.IsType<ServiceService>(result);
        }

        [Fact]
        public void TestUpdate_ReturnServiceService(){
            unitOfWork.Setup(x => x.ServiceRepository.Update(It.IsAny<Service>())).Returns(true);

            var result = serviceService.Update(list[0], list[1]);

            Assert.IsType<ServiceService>(result);
        }

        [Fact] void TestDelete_ReturnServiceService(){
            unitOfWork.Setup(x => x.ServiceRepository.Delete(It.IsAny<Service>())).Returns(true);

            var result = serviceService.Delete(list[0]);

            Assert.IsType<ServiceService>(result);
        }
    }
}