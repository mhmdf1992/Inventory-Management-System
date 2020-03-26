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
        EntityService<User> entityService;
        IUserService userService;
        List<User> list;
        PagedList<User> pagedList;
        IUserCredentials userCredentials;
        public UserServiceTest(){
            unitOfWork = new Mock<IUnitOfWork>();
            entityService = new UserService(unitOfWork.Object);
            userService = new UserService(unitOfWork.Object);
            list = new List<User>(){
                new User() {Id = 1, Firstname="Mhmd", Lastname="Fayad", Email = "mhmdfayad@gmail.com", Password="admin"},
                    new User() {Id = 2, Firstname="Jackie", Lastname="Chan", Email = "jackie_chan@gmail.com", Password="102030"},
                    new User() {Id = 3, Firstname="Thomas", Lastname="Party", Email = "thomasp1919@gmail.com", Password="405060"}
            };
            userCredentials = new User(){Email = "mhmdfayad@gmail.com", Password = "admin"};
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

            var result = entityService.Get(skip, take);

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

            var result = entityService.FindMatch(list[0], 0, 1);

            Assert.IsType<PagedList<User>>(result);
            Assert.Equal(result.Count, 1);
            Assert.Equal(result.Total, list.Count);
        }

        [Fact]
        public void TestGet_ReturnUser(){
            unitOfWork.Setup(x => x.UserRepository.Get(It.IsAny<long>())).Returns(list[0]);
            
            var result = entityService.Get(1);

            Assert.IsType<User>(result);
        }

        [Fact]
        public void TestInsert_ReturnUserService(){
            unitOfWork.Setup(x => x.UserRepository.Insert(It.IsAny<User>())).Returns(true);

            var result = entityService.Insert(list[0]);

            Assert.IsType<UserService>(result);
        }

        [Fact]
        public void TestUpdate_ReturnUserService(){
            unitOfWork.Setup(x => x.UserRepository.Update(It.IsAny<User>())).Returns(true);

            var result = entityService.Update(list[0], list[1]);

            Assert.IsType<UserService>(result);
        }

        [Fact] void TestDelete_ReturnUserService(){
            unitOfWork.Setup(x => x.UserRepository.Delete(It.IsAny<User>())).Returns(true);

            var result = entityService.Delete(list[0]);

            Assert.IsType<UserService>(result);
        }

        [Fact]
        public void TestRegister_ReturnUserService(){
            unitOfWork.Setup(x => x.UserRepository.Insert(It.IsAny<User>())).Returns(true);
            
            var result = userService.Register(list[0]);

            Assert.IsType<UserService>(result);
        }

        [Fact]
        public void TestAuthenticate_ReturnUserIfUserCredExist(){
            unitOfWork.Setup(
                x => x.UserRepository.Get( It.IsAny<Expression<Func<User, bool>>>(),
                 It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                 It.IsAny<string>())).Returns(list);
            
            var result = userService.Authenticate(userCredentials);

            Assert.IsType<User>(result);
        }

        [Fact]
        public void TestAuthenticate_ReturnNullIfUserCredNotExist(){
            unitOfWork.Setup(
                x => x.UserRepository.Get( It.IsAny<Expression<Func<User, bool>>>(),
                 It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                 It.IsAny<string>())).Returns(new List<User>());
            
            var result = userService.Authenticate(userCredentials);

            Assert.Null(result);
        }

        [Fact]
        public void TestGet_ReturnUserIfEmailExist(){
            unitOfWork.Setup(
                x => x.UserRepository.Get( It.IsAny<Expression<Func<User, bool>>>(),
                 It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = userService.Get(userCredentials.Email);

            Assert.IsType<User>(result);
        }

        [Fact]
        public void TestGet_ReturnNullIfEmailNotExist(){
            unitOfWork.Setup(
                x => x.UserRepository.Get( It.IsAny<Expression<Func<User, bool>>>(),
                 It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                 It.IsAny<string>())).Returns(new List<User>());

            var result = userService.Get(userCredentials.Email);

            Assert.Null(result);
        }

        [Fact]
        public void TestExist_ReturnTrueIfEmailExist(){
            unitOfWork.Setup(
                x => x.UserRepository.Get( It.IsAny<Expression<Func<User, bool>>>(),
                 It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = userService.Exist(userCredentials.Email);

            Assert.True(result);
        }

        [Fact]
        public void TestExist_ReturnFalseIfEmailNotExist(){
            unitOfWork.Setup(
                x => x.UserRepository.Get( It.IsAny<Expression<Func<User, bool>>>(),
                 It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                 It.IsAny<string>())).Returns(new List<User>());

            var result = userService.Exist(userCredentials.Email);

            Assert.False(result);
        }
    }
}