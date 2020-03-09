namespace InventoryManagementSystem.Api.Models.Contact.Client
{
    public class Client : Contact, IAppDbContextEntity
    {
        public long Id {get; set;}
        public bool IsDeleted {get; set;}
    }
}