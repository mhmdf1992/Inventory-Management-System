using InventoryManagementSystem.Api.DAL.Repository;
using InventoryManagementSystem.Api.Data;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Models.Product.Tangible;

namespace InventoryManagementSystem.Api.DAL.UnitOfWork
{
    public class AppDbContextUnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext dbContext;
        protected readonly IRepository<Item> itemRepository;
        protected readonly IRepository<Service> serviceRepository;
        public AppDbContextUnitOfWork(IRepository<Item> itemRepository,
            IRepository<Service> serviceRepository,
            AppDbContext dbContext)
        {
            this.itemRepository = itemRepository;
            this.serviceRepository = serviceRepository;
            this.dbContext = dbContext;
        }
        public IRepository<Item> ItemRepository
        {
            get 
            {
                return this.itemRepository;
            }
        }
        public IRepository<Service> ServiceRepository
        {
            get 
            {
                return this.serviceRepository;
            }
        }

        public int Save()
        {
            return this.dbContext.SaveChanges();
        }
    }
}