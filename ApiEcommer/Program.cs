// Importa las extensiones de Entity Framework Core
using ApiEcommer.Repository;
using ApiEcommer.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

// Crea el builder para configurar la aplicación web
var builder = WebApplication.CreateBuilder(args);

// Obtiene la cadena de conexión desde appsettings.json
var dbConnection = builder.Configuration.GetConnectionString("ConexionSql");

// Registra el contexto de base de datos en el contenedor de dependencias
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnection));

// Inyecta el repositorio de categorías
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Registra AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Registra los controladores de la API
builder.Services.AddControllers();

// Configura Swagger para documentación de la API (solo en desarrollo)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Construye la aplicación con todas las configuraciones
var app = builder.Build();

// Configura el pipeline de middleware (orden importa)
// Habilita Swagger siempre para pruebas
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Ecommerce V1");
});

// Redirige HTTP a HTTPS automáticamente
app.UseHttpsRedirection();

// Habilita autorización (aunque no esté configurada aún)
app.UseAuthorization();

// Mapea los controladores a las rutas
app.MapControllers();

// Inicia la aplicación
app.Run();

