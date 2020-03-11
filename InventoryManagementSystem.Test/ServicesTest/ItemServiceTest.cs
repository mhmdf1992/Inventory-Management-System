using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.Services;
using Moq;
using Xunit;

namespace InventoryManagementSystem.Test.ServicesTest
{
    public class ItemServiceTest
    {
        Mock<IUnitOfWork> unitOfWork;
        EntityService<Item> itemService;
        List<Item> list;
        PagedList<Item> pagedList;
        public ItemServiceTest(){
            unitOfWork = new Mock<IUnitOfWork>();
            itemService = new ItemService(unitOfWork.Object);
            list = new List<Item>(){
                new Item(){Id = 1, Description = "snips 25g", Code = "0001", Price = 0.5},
                new Item(){Id = 2, Description = "snips 50g", Code = "0002", Price = 1},
                new Item(){Id = 3, Description = "snips 100g", Code = "0003", Price = 1.5},
            };
            pagedList = new PagedList<Item>(list);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(0, 3)]
        public void TestGet_ReturnPagedListOfItem_ListCountEqualsTake_TotalEqualsQueriedListCount(int skip, int take){
            unitOfWork.Setup(
                x => x.ItemRepository.Get( It.IsAny<Expression<Func<Item, bool>>>(),
                 It.IsAny<Func<IQueryable<Item>, IOrderedQueryable<Item>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = itemService.Get(skip, take);

            Assert.IsType<PagedList<Item>>(result);
            Assert.Equal(result.Count, take);
            Assert.Equal(result.Total, list.Count);
        }

        [Fact]
        public void TestFindMatch_ReturnPagedListOfItem_ListCountEqualsTake_TotalEqualsQueriedListCount(){
            unitOfWork.Setup(
                x => x.ItemRepository.Get( It.IsAny<Expression<Func<Item, bool>>>(),
                 It.IsAny<Func<IQueryable<Item>, IOrderedQueryable<Item>>>(),
                 It.IsAny<string>())).Returns(list);

            var result = itemService.FindMatch(list[0], 0, 1);

            Assert.IsType<PagedList<Item>>(result);
            Assert.Equal(result.Count, 1);
            Assert.Equal(result.Total, list.Count);
        }

        [Fact]
        public void TestGet_ReturnItem(){
            unitOfWork.Setup(x => x.ItemRepository.Get(It.IsAny<long>())).Returns(list[0]);
            
            var result = itemService.Get(1);

            Assert.IsType<Item>(result);
        }

        [Fact]
        public void TestInsert_ReturnItemService(){
            unitOfWork.Setup(x => x.ItemRepository.Insert(It.IsAny<Item>())).Returns(true);

            var result = itemService.Insert(list[0]);

            Assert.IsType<ItemService>(result);
        }

        [Fact]
        public void TestUpdate_ReturnItemService(){
            unitOfWork.Setup(x => x.ItemRepository.Update(It.IsAny<Item>())).Returns(true);

            var result = itemService.Update(list[0], list[1]);

            Assert.IsType<ItemService>(result);
        }

        [Fact] void TestDelete_ReturnItemService(){
            unitOfWork.Setup(x => x.ItemRepository.Delete(It.IsAny<Item>())).Returns(true);

            var result = itemService.Delete(list[0]);

            Assert.IsType<ItemService>(result);
        }
    }
}