using InventoryManagementSystem.Api.DAL.Repository;
using InventoryManagementSystem.Api.Data;
using InventoryManagementSystem.Api.Models.Contact.Client;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.DAL.UnitOfWork
{
    public class EntitiesUnitOfWork : IUnitOfWork
    {
        protected readonly AppDbContext dbContext;
        protected readonly IRepository<Item> itemRepository;
        protected readonly IRepository<Service> serviceRepository;
        protected readonly IRepository<Supplier> supplierRepository;
        protected readonly IRepository<Client> clientRepository;
        protected readonly IRepository<User> userRepository;
        public EntitiesUnitOfWork(IRepository<Item> itemRepository,
            IRepository<Service> serviceRepository,
            IRepository<Supplier> supplierRepository,
            IRepository<Client> clientRepository,
            IRepository<User> userRepository,
            AppDbContext dbContext){
            this.itemRepository = itemRepository;
            this.serviceRepository = serviceRepository;
            this.supplierRepository = supplierRepository;
            this.clientRepository = clientRepository;
            this.userRepository = userRepository;
            this.dbContext = dbContext;
        }
        public IRepository<Item> ItemRepository{
            get {
                return this.itemRepository;
            }
        }
        public IRepository<Service> ServiceRepository{
            get {
                return this.serviceRepository;
            }
        }

        public IRepository<Supplier> SupplierRepository{
            get{
                return this.supplierRepository;
            }
        }

        public IRepository<Client> ClientRepository{
            get {
                return this.clientRepository;
            }
        }

        public IRepository<User> UserRepository{
            get {
                return this.userRepository;
            }
        }

        public int Save(){
            return this.dbContext.SaveChanges();
        }
    }
}