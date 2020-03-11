namespace InventoryManagementSystem.Api.Models.User
{
    public class User : IEntity
    {
        public long Id {get; set;}
        public bool IsDeleted {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
    }
}