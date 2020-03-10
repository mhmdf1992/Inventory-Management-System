namespace InventoryManagementSystem.Api.Models
{
    public interface IEntity
    {
        long Id {get; set;}
        bool IsDeleted {get; set;}
    }
}