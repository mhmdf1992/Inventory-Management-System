namespace InventoryManagementSystem.Api.Models.Product
{
    public class Service : Product, ISellable, IAppDbContextEntity
    {
        public long Id {get; set;}
        public double Price {get; set;}
        public bool IsDeleted {get; set;}
    }
}