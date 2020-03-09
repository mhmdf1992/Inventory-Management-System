namespace InventoryManagementSystem.Api.Models.Product
{
    public class Service : Product, IAppDbContextEntity
    {
        public long Id {get; set;}
        public bool IsDeleted {get; set;}
    }
}