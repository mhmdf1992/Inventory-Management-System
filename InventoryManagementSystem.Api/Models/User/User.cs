namespace InventoryManagementSystem.Api.Models.User
{
    public class User : IUserCredentials, IEntity
    {
        public long Id {get; set;}
        public bool IsDeleted {get; set;}
        public string Firstname {get; set;}
        public string Lastname {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
    }
}