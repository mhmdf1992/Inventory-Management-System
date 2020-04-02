using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Models.Contact.Client;
using InventoryManagementSystem.Api.Services;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ServicesTest
{
    public class ClientServiceTest
    {
        Mock<IUnitOfWork> unitOfWork;
        EntityService<Client> clientService;
        List<Client> list;
        PagedList<Client> pagedList;
        public ClientServiceTest(){
            unitOfWork = new Mock<IUnitOfWork>();
            clientService = new ClientService(unitOfWork.Object);
            list = new List<Client>(){
                new Client() {Id = 1, Name = "Marco Verati", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new Client() {Id = 2, Name = "Benjamin Stone", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                new Client() {Id = 3, Name = "Bernardo Silva", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"}
            };
            pagedList = new PagedList<Client>(list);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(0, 3)]
        public void TestGet_ReturnPagedListOfClient_ListCountEqualsTake_TotalEqualsQueriedListCount(int skip, int take){
            unitOfWork.Setup(
                x => x.ClientRepository.Get( It.IsAny<Expression<Func<Client, bool>>>(),
                 It.IsAny<Func<IQueryable<Client>, IOrderedQueryable<Client>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = clientService.Get(skip, take);

            Assert.IsType<PagedList<Client>>(result);
            Assert.Equal(result.Count, take);
            Assert.Equal(result.Total, list.Count);
        }

        [Fact]
        public void TestFindMatch_ReturnPagedListOfClient_ListCountEqualsTake_TotalEqualsQueriedListCount(){
            unitOfWork.Setup(
                x => x.ClientRepository.Get( It.IsAny<Expression<Func<Client, bool>>>(),
                 It.IsAny<Func<IQueryable<Client>, IOrderedQueryable<Client>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = clientService.FindMatch(list[0], 0, 1);

            Assert.IsType<PagedList<Client>>(result);
            Assert.Single(result);
            Assert.Equal(list.Count, result.Total);
        }

        [Fact]
        public void TestFindMatchString_ReturnPagedListOfClient_ListCountEqualsTake_TotalEqualsQueriedListCount(){
            unitOfWork.Setup(
                x => x.ClientRepository.Get( It.IsAny<Expression<Func<Client, bool>>>(),
                 It.IsAny<Func<IQueryable<Client>, IOrderedQueryable<Client>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = clientService.FindMatch("marco", 0, 1);

            Assert.IsType<PagedList<Client>>(result);
            Assert.Single(result);
            Assert.Equal(list.Count, result.Total);
        }

        [Fact]
        public void TestGet_ReturnClient(){
            unitOfWork.Setup(x => x.ClientRepository.Get(It.IsAny<long>())).Returns(list[0]);
            
            var result = clientService.Get(1);

            Assert.IsType<Client>(result);
        }

        [Fact]
        public void TestInsert_ReturnClientService(){
            unitOfWork.Setup(x => x.ClientRepository.Insert(It.IsAny<Client>())).Returns(true);

            var result = clientService.Insert(list[0]);

            Assert.IsType<ClientService>(result);
        }

        [Fact]
        public void TestUpdate_ReturnClientService(){
            unitOfWork.Setup(x => x.ClientRepository.Update(It.IsAny<Client>())).Returns(true);

            var result = clientService.Update(list[0], list[1]);

            Assert.IsType<ClientService>(result);
        }

        [Fact] void TestDelete_ReturnClientService(){
            unitOfWork.Setup(x => x.ClientRepository.Delete(It.IsAny<Client>())).Returns(true);

            var result = clientService.Delete(list[0]);

            Assert.IsType<ClientService>(result);
        }
    }
}