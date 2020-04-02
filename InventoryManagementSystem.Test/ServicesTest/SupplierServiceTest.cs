using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using InventoryManagementSystem.Api.Services;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ServicesTest
{
    public class SupplierServiceTest
    {
        Mock<IUnitOfWork> unitOfWork;
        EntityService<Supplier> supplierService;
        List<Supplier> list;
        PagedList<Supplier> pagedList;
        public SupplierServiceTest(){
            unitOfWork = new Mock<IUnitOfWork>();
            supplierService = new SupplierService(unitOfWork.Object);
            list = new List<Supplier>(){
                new Supplier() {Id = 1, Name = "Marco Verati", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new Supplier() {Id = 2, Name = "Benjamin Stone", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new Supplier() {Id = 3, Name = "Bernardo Silva", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"}
            };
            pagedList = new PagedList<Supplier>(list);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(0, 3)]
        public void TestGet_ReturnPagedListOfSupplier_ListCountEqualsTake_TotalEqualsQueriedListCount(int skip, int take){
            unitOfWork.Setup(
                x => x.SupplierRepository.Get( It.IsAny<Expression<Func<Supplier, bool>>>(),
                 It.IsAny<Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = supplierService.Get(skip, take);

            Assert.IsType<PagedList<Supplier>>(result);
            Assert.Equal(result.Count, take);
            Assert.Equal(result.Total, list.Count);
        }

        [Fact]
        public void TestFindMatch_ReturnPagedListOfSupplier_ListCountEqualsTake_TotalEqualsQueriedListCount(){
            unitOfWork.Setup(
                x => x.SupplierRepository.Get( It.IsAny<Expression<Func<Supplier, bool>>>(),
                 It.IsAny<Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = supplierService.FindMatch(list[0], 0, 1);

            Assert.IsType<PagedList<Supplier>>(result);
            Assert.Single(result);
            Assert.Equal(list.Count, result.Total);
        }

        [Fact]
        public void TestFindMatchString_ReturnPagedListOfSupplier_ListCountEqualsTake_TotalEqualsQueriedListCount(){
            unitOfWork.Setup(
                x => x.SupplierRepository.Get( It.IsAny<Expression<Func<Supplier, bool>>>(),
                 It.IsAny<Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = supplierService.FindMatch("marco", 0, 1);

            Assert.IsType<PagedList<Supplier>>(result);
            Assert.Single(result);
            Assert.Equal(list.Count, result.Total);
        }

        [Fact]
        public void TestGet_ReturnSupplier(){
            unitOfWork.Setup(x => x.SupplierRepository.Get(It.IsAny<long>())).Returns(list[0]);
            
            var result = supplierService.Get(1);

            Assert.IsType<Supplier>(result);
        }

        [Fact]
        public void TestInsert_ReturnSupplierService(){
            unitOfWork.Setup(x => x.SupplierRepository.Insert(It.IsAny<Supplier>())).Returns(true);

            var result = supplierService.Insert(list[0]);

            Assert.IsType<SupplierService>(result);
        }

        [Fact]
        public void TestUpdate_ReturnSupplierService(){
            unitOfWork.Setup(x => x.SupplierRepository.Update(It.IsAny<Supplier>())).Returns(true);

            var result = supplierService.Update(list[0], list[1]);

            Assert.IsType<SupplierService>(result);
        }

        [Fact] void TestDelete_ReturnSupplierService(){
            unitOfWork.Setup(x => x.SupplierRepository.Delete(It.IsAny<Supplier>())).Returns(true);

            var result = supplierService.Delete(list[0]);

            Assert.IsType<SupplierService>(result);
        }
    }
}