using System.ComponentModel.DataAnnotations;
using InventoryManagementSystem.Api.Services;

namespace InventoryManagementSystem.Api.DTOs.ValidationAttributes
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext){
            return IsValid(value.ToString(),
                ((IUserService)validationContext.GetService(typeof(IUserService))),
                (UserDTO)validationContext.ObjectInstance)
                    ? ValidationResult.Success
                    : new ValidationResult(FormatErrorMessage((validationContext.DisplayName)));
        }

        public bool IsValid(string email, IUserService service, UserDTO model){
            return !service.Exist(email) || model.id == service.Get(email).Id;
        }
    }
}