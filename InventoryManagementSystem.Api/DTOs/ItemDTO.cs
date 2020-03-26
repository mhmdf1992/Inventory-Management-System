using System.ComponentModel.DataAnnotations;
using InventoryManagementSystem.Api.DTOs.ValidationAttributes;

namespace InventoryManagementSystem.Api.DTOs
{
    public class ItemDTO
    {
        
        public long id {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Code is too short")]
        [MaxLength(25, ErrorMessage = "Code is too long")]
        [UniqueItemCode(ErrorMessage = "Code already exist")]
        public string code {get; set;}

        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Description is too short")]
        [MaxLength(250, ErrorMessage = "Description is too long")]
        public string description {get; set;}

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Currency)]
        public double price {get; set;}

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^data:image\/([a-zA-Z]*);base64,(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$",ErrorMessage = "Image is invalid")]
        public string imageBase64 {get; set;}
    }
}