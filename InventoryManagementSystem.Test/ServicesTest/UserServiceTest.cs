using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Models.User;
using InventoryManagementSystem.Api.Services;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ServicesTest
{
    public class UserServiceTest
    {
        Mock<IUnitOfWork> unitOfWork;
        EntityService<User> userService;
        IUserService userRelService;
        List<User> list;
        PagedList<User> pagedList;
        public UserServiceTest(){
            unitOfWork = new Mock<IUnitOfWork>();
            userService = new UserService(unitOfWork.Object);
            userRelService = new UserService(unitOfWork.Object);
            list = new List<User>(){
                new User() {Id = 1, Username = "Marco@Verati.com", Password = "9613822106"},
                new User() {Id = 2, Username = "Benjamin@Stone.com", Password = "9613822 106"},
                new User() {Id = 3, Username = "Bernardo@Silva.com", Password = "9613822 106"}
            };
            pagedList = new PagedList<User>(list);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(0, 3)]
        public void TestGet_ReturnPagedListOfUser_ListCountEqualsTake_TotalEqualsQueriedListCount(int skip, int take){
            unitOfWork.Setup(
                x => x.UserRepository.Get( It.IsAny<Expression<Func<User, bool>>>(),
                 It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = userService.Get(skip, take);

            Assert.IsType<PagedList<User>>(result);
            Assert.Equal(result.Count, take);
            Assert.Equal(result.Total, list.Count);
        }

        [Fact]
        public void TestFindMatch_ReturnPagedListOfUser_ListCountEqualsTake_TotalEqualsQueriedListCount(){
            unitOfWork.Setup(
                x => x.UserRepository.Get( It.IsAny<Expression<Func<User, bool>>>(),
                 It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = userService.FindMatch(list[0], 0, 1);

            Assert.IsType<PagedList<User>>(result);
            Assert.Equal(result.Count, 1);
            Assert.Equal(result.Total, list.Count);
        }

        [Fact]
        public void TestFind_ReturnUser(){
            unitOfWork.Setup(
                x => x.UserRepository.Get( It.IsAny<Expression<Func<User, bool>>>(),
                 It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                 It.IsAny<string>())).Returns(list);
            
            var result = userRelService.Find(list[0]);

            Assert.IsType<User>(result);
        }

        [Fact]
        public void TestGet_ReturnUser(){
            unitOfWork.Setup(x => x.UserRepository.Get(It.IsAny<long>())).Returns(list[0]);
            
            var result = userService.Get(1);

            Assert.IsType<User>(result);
        }

        [Fact]
        public void TestInsert_ReturnUserService(){
            unitOfWork.Setup(x => x.UserRepository.Insert(It.IsAny<User>())).Returns(true);

            var result = userService.Insert(list[0]);

            Assert.IsType<UserService>(result);
        }

        [Fact]
        public void TestUpdate_ReturnUserService(){
            unitOfWork.Setup(x => x.UserRepository.Update(It.IsAny<User>())).Returns(true);

            var result = userService.Update(list[0], list[1]);

            Assert.IsType<UserService>(result);
        }

        [Fact] void TestDelete_ReturnUserService(){
            unitOfWork.Setup(x => x.UserRepository.Delete(It.IsAny<User>())).Returns(true);

            var result = userService.Delete(list[0]);

            Assert.IsType<UserService>(result);
        }
    }
}