using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Api.DTOs
{
    public class ClientDTO
    {
        public long id {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Name is too short")]
        [MaxLength(50, ErrorMessage = "Name is too long")]
        public string name {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Location is too short")]
        [MaxLength(250, ErrorMessage = "Location is too long")]
        public string location {get; set;}

        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        public string email {get; set;}

        [Phone(ErrorMessage = "Telephone is invalid")]
        public string telephone {get; set;}
    }
}