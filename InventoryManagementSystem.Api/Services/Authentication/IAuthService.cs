using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.Services.Auth
{
    public interface IAuthService
    {
        string Authenticate(IUserCredentials userCred);
        string Register(User user);
    }
}