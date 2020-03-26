using System.ComponentModel.DataAnnotations;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.Services;

namespace InventoryManagementSystem.Api.DTOs.ValidationAttributes
{
    public class UniqueItemCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext){
            return IsValid(value.ToString(), 
                ((IProductService<Item>)validationContext.GetService(typeof(IProductService<Item>))),
                (ItemDTO)validationContext.ObjectInstance)
                ? ValidationResult.Success
                : new ValidationResult(FormatErrorMessage((validationContext.DisplayName)));
        }

        public bool IsValid(string code, IProductService<Item> service, ItemDTO model){
            return !service.Exist(code) || model.id == service.Get(code).Id;
        }
    }
}