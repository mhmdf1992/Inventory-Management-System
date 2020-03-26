using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.Services
{
    public interface IUserService
    {
        bool Exist(string email);
        User Get(string email);
        User Authenticate(IUserCredentials userCred);
        EntityService<User> Register(User user);
    }
}