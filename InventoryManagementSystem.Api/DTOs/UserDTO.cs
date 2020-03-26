using System.ComponentModel.DataAnnotations;
using InventoryManagementSystem.Api.DTOs.ValidationAttributes;
namespace InventoryManagementSystem.Api.DTOs
{
    public class UserDTO
    {
        public long id {get; set;}

        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        [UniqueEmail(ErrorMessage = "Email Address already exist")]
        public string email {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Firstname is too short")]
        [MaxLength(25, ErrorMessage = "Firstname is too long")]
        public string firstname {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Lastname is too short")]
        [MaxLength(25, ErrorMessage = "Lastname is too long")]
        public string lastname {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(6, ErrorMessage = "Password is too short")]
        [MaxLength(25, ErrorMessage = "Password is too long")]
        public string password {get; set;}
    }
}