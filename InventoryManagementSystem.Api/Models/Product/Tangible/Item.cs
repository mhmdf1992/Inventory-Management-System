namespace InventoryManagementSystem.Api.Models.Product.Tangible
{
    public class Item : Product, ITangible, IEntity
    {
        public long Id {get; set;}
        public string ImageBase64 {get; set;}
        public bool IsDeleted {get; set;}
    }
}