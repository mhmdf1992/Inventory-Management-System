using InventoryManagementSystem.Api.Models.Product;

namespace InventoryManagementSystem.Api.Services
{
    public interface IProductService<T> where T : Product
    {
        bool Exist(string code);
        T Get(string code);
    }
}