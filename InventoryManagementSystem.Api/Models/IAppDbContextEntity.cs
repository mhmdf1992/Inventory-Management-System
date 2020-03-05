namespace InventoryManagementSystem.Api.Models
{
    public interface IAppDbContextEntity
    {
        long Id {get; set;}
        bool IsDeleted {get; set;}
    }
}