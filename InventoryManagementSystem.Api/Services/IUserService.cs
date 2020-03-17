using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.Services
{
    public interface IUserService
    {
        User Authenticate(IUserCredentials userCred);
        EntityService<User> Register(User user);
    }
}