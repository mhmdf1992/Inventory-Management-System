using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Api.DTOs
{
    public class UserCredentialsDTO
    {
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        public string email {get; set;}
        [Required(ErrorMessage = "Required")]
        public string password {get; set;}
    }
}