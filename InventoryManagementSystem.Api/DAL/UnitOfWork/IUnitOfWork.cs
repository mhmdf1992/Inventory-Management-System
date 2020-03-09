using InventoryManagementSystem.Api.DAL.Repository;
using InventoryManagementSystem.Api.Models.Contact.Client;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Models.Product.Tangible;

namespace InventoryManagementSystem.Api.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Item> ItemRepository {get;}
        IRepository<Service> ServiceRepository {get;}
        IRepository<Supplier> SupplierRepository {get;}
        IRepository<Client> ClientRepository {get;}
        int Save();
    }
}