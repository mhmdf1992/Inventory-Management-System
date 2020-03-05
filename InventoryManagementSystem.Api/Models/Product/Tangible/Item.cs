namespace InventoryManagementSystem.Api.Models.Product.Tangible
{
    public class Item : Product, ISellable, ITangible, IAppDbContextEntity
    {
        public long Id {get; set;}
        public string ImageBase64 {get; set;}
        public double Price {get; set;}
        public bool IsDeleted {get; set;}
    }
}