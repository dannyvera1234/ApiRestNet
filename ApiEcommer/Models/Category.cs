using System.ComponentModel.DataAnnotations;

public class Category
{
    // [Key] marca esta propiedad como clave primaria
    // Entity Framework la configurará como IDENTITY (auto-incremento)
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public int Price { get; set; }
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime CreationDate { get; set; }
}