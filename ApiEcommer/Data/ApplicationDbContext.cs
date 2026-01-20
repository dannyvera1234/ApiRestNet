// Importa las clases necesarias de Entity Framework Core
using ApiEcommer.Models;
using Microsoft.EntityFrameworkCore;

// Clase que representa el contexto de la base de datos
// Hereda de DbContext que es la clase base de EF Core
public class ApplicationDbContext : DbContext
{
    // Constructor que recibe las opciones de configuración de la BD
    // y las pasa a la clase base DbContext
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // DbSet representa una tabla en la base de datos
    // Categories será el nombre de la tabla para la entidad Category
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
}
