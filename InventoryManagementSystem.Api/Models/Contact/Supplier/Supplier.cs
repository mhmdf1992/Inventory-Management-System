namespace InventoryManagementSystem.Api.Models.Contact.Supplier
{
    public class Supplier : Contact, IAppDbContextEntity
    {
        public long Id {get; set;}
        public bool IsDeleted {get; set;}
    }
}