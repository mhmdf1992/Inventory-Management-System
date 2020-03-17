namespace InventoryManagementSystem.Api.Models.User
{
    public interface IUserCredentials
    {
        string Email {get; set;}
        string Password {get; set;}
    }
}