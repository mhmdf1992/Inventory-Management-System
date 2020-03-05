using System;
using InventoryManagementSystem.Api.DAL.Repository;
using InventoryManagementSystem.Api.Data;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InventoryManagementSystem.Test
{
     public class AppDbContextRepositoryTest
    {
        AppDbContext dbContext;
        IRepository<Item> itemRepository;
        Item item;
        public AppDbContextRepositoryTest()
        {
            dbContext = new AppDbContext(
                new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options
            );
            itemRepository = new AppDbContextRepository<Item>(dbContext);
            item = new Item() {Id = 1, Description = "snips 25g", Code = "0001", Price = 0.5};
        }

        [Fact]
        public void TestInsert()
        {
            Assert.True(itemRepository.Insert(item));
        }

        [Fact]
        public void TestUpdate()
        {
            itemRepository.Insert(item);
            item.Description = "Chackib";
            Assert.True(itemRepository.Update(item));
        }

        [Fact]
        public void TestDelete()
        {
            itemRepository.Insert(item);
            Assert.True(itemRepository.Delete(item));
        }

        [Fact]
        public void TestGet()
        {
            itemRepository.Insert(item);
            dbContext.SaveChanges();
            Assert.Contains(item, itemRepository.Get());
        }

        [Fact]
        public void TestGetById()
        {
            itemRepository.Insert(item);
            dbContext.SaveChanges();
            Assert.Equal(item, itemRepository.Get(item.Id));
        }

        [Fact]
        public void TestAny()
        {
            itemRepository.Insert(item);
            dbContext.SaveChanges();
            Assert.True(itemRepository.Any());
        }
    }
}
