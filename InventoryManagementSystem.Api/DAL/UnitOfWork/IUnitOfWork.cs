
using InventoryManagementSystem.Api.DAL.Repository;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Models.Product.Tangible;

namespace InventoryManagementSystem.Api.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Item> ItemRepository {get;}
        IRepository<Service> ServiceRepository {get;}
        int Save();
    }
}