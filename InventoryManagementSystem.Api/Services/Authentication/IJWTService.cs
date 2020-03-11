using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.Services.Auth
{
    public interface IJWTService
    {
        string GenerateToken(User user);
    }
}