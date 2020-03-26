using System.ComponentModel.DataAnnotations;
using InventoryManagementSystem.Api.DTOs.ValidationAttributes;

namespace InventoryManagementSystem.Api.DTOs
{
    public class ServiceDTO
    {
        public long id {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Code is too short")]
        [MaxLength(25, ErrorMessage = "Code is too long")]
        [UniqueServiceCode(ErrorMessage = "Code already exist")]
        public string code {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Description is too short")]
        [MaxLength(25, ErrorMessage = "Description is too long")]
        public string description {get; set;}

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Currency)]
        public double price {get; set;}
    }
}