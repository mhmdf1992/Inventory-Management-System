using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.Services.Auth
{
    public interface IAuthService
    {
        string Authenticate(User user);
    }
}