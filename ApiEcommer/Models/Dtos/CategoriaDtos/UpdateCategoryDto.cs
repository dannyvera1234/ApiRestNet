using System.ComponentModel.DataAnnotations;

namespace ApiEcommer.Models.Dtos
{
    /// <summary>
    /// DTO para actualizar una categoría existente
    /// NO incluye ID porque viene en la URL
    /// </summary>
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(60, ErrorMessage = "Name cannot exceed 60 characters")]
        [MinLength(3, ErrorMessage = "Name must have at least 3 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [MinLength(3, ErrorMessage = "Description must have at least 3 characters")]
        [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
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
        public int Price { get; set; }
    }
}