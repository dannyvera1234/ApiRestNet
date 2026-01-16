using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEcommer.Models.Dtos.ProductDtos
{
    public class UpdateProductDto
    {
        [Required(ErrorMessage = "Name es requerido")]
        [MaxLength(60, ErrorMessage = "maximo 60 caracteres")]
        [MinLength(3, ErrorMessage = "minimo 3 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description es requerido")]
        [MaxLength(60, ErrorMessage = "maximo 60 caracteres")]
        [MinLength(3, ErrorMessage = "minimo 3 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price es requerido")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must have up to 2 decimal places")]
        [DataType(DataType.Currency)] // Para mostrar el campo como moneda
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)] // Para mostrar el campo como moneda]
        [Display(Name = "Price")] // Para mostrar el nombre del campo como "Precio"
        public decimal Price { get; set; }
        public string ImgUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "Stock es requerido")]
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string SKU { get; set; } = string.Empty;
    }
}