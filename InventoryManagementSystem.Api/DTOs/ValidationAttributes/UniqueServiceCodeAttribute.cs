using System.ComponentModel.DataAnnotations;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Services;

namespace InventoryManagementSystem.Api.DTOs.ValidationAttributes
{
    public class UniqueServiceCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext){
            return IsValid(value.ToString(),
                ((IProductService<Service>)validationContext.GetService(typeof(IProductService<Service>))),
                (ServiceDTO)validationContext.ObjectInstance)
                ? ValidationResult.Success
                : new ValidationResult(FormatErrorMessage((validationContext.DisplayName)));
        }

        public bool IsValid(string code, IProductService<Service> service, ServiceDTO model){
            return !service.Exist(code) || model.id == service.Get(code).Id;
        }
    }
}