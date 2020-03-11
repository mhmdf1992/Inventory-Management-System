using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.Services
{
    public interface IUserService
    {
        User Find(User user);
    }
}