namespace InventoryManagementSystem.Api.Models.Contact.Supplier
{
    public class Supplier : Contact, IEntity
    {
        public long Id {get; set;}
        public bool IsDeleted {get; set;}
    }
}