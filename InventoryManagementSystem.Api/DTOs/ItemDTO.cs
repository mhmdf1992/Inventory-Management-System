namespace InventoryManagementSystem.Api.DTOs
{
    public class ItemDTO
    {
        
        public long Id {get; set;}
        public string Code {get; set;}
        public string Description {get; set;}
        public double Price {get; set;}
        public string ImageBase64 {get; set;}
    }
}