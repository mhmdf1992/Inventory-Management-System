namespace InventoryManagementSystem.Api.Models.Contact.Client
{
    public class Client : Contact, IEntity
    {
        public long Id {get; set;}
        public bool IsDeleted {get; set;}
    }
}