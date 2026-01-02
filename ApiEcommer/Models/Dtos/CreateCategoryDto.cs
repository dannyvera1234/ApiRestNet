using System.ComponentModel.DataAnnotations; // Para usar atributos de validación

namespace ApiEcommer.Models.Dtos
{
    /// <summary>
    /// DTO (Data Transfer Object) para crear una nueva categoría
    /// Los DTOs se usan para transferir datos entre capas de la aplicación
    /// </summary>
    public class CreateCategoryDto
    {
        /// <summary>
        /// Nombre de la categoría a crear
        /// </summary>
        [Required(ErrorMessage = "Name is required")] // Campo obligatorio
        [MaxLength(60, ErrorMessage = "Name cannot exceed 60 characters")] // Máximo 60 caracteres
        [MinLength(3, ErrorMessage = "Name must have at least 3 characters")] // Mínimo 3 caracteres
        public string Name { get; set; } = string.Empty; // Inicializado como cadena vacía

        // Agrega más propiedades conforme necesites
        /// <summary>
        /// Descripción de la categoría
        /// </summary>
        [Required(ErrorMessage = "Description is required")] // Campo obligatorio
        [MinLength(3, ErrorMessage = "Description must have at least 3 characters")] // Mínimo 3 caracteres 
        [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")] // Máximo 100 caracteres
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Address is required")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must have up to 2 decimal places")]
        [DataType(DataType.Currency)] // Para mostrar el campo como moneda
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)] // Para mostrar el campo como moneda]
        [Display(Name = "Price")] // Para mostrar el nombre del campo como "Precio"
        public int Price { get; set; }

    }
}