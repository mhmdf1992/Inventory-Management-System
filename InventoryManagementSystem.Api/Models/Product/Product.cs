namespace InventoryManagementSystem.Api.Models.Product
{
    public abstract class Product 
    {
        public string Code {get; set;}
        public string Description {get; set;}
        public double Price {get; set;}
    }
}