# Comandos Útiles - API Ecommerce .NET 8

## Docker Commands (SQL Server)

### Levantar contenedor SQL Server
```bash
docker-compose up -d
```

### Detener contenedor
```bash
docker-compose down
```

### Detener y eliminar volúmenes (resetear BD)
```bash
docker-compose down -v
```

### Ver logs del contenedor
```bash
docker logs sqlserver2022
```

### Ver contenedores corriendo
```bash
docker ps
```

### Conectar a SQL Server desde contenedor
```bash
docker exec sqlserver2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -C -Q "SELECT @@VERSION"
```

## Entity Framework Commands

### Instalar herramientas EF globalmente
```bash
dotnet tool install --global dotnet-ef --version 8.0.11
```

### Crear migración
```bash
dotnet ef migrations add NombreMigracion
```

### Aplicar migraciones a la BD
```bash
dotnet ef database update
```

### Listar migraciones
```bash
dotnet ef migrations list
```

### Eliminar última migración
```bash
dotnet ef migrations remove
```

### Generar script SQL de migración
```bash
dotnet ef migrations script
```

## .NET Commands

### Restaurar paquetes
```bash
dotnet restore
```

### Compilar proyecto
```bash
dotnet build
```

### Ejecutar aplicación
```bash
dotnet run
```

### Agregar paquete NuGet
```bash
dotnet add package NombrePaquete --version X.X.X
```

### Ver paquetes instalados
```bash
dotnet list package
```

## Configuración de Conexión

### Cadena de conexión en appsettings.json
```json
{
  "ConnectionStrings": {
    "ConexionSql": "Server=localhost,1433;Database=ApiEcommerNET8;User ID=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

### Credenciales SQL Server (Docker)
- **Usuario:** sa
- **Contraseña:** YourStrong@Passw0rd
- **Puerto:** 1433
- **Base de datos:** ApiEcommerNET8

## Paquetes Instalados

- Microsoft.EntityFrameworkCore (8.0.11)
- Microsoft.EntityFrameworkCore.SqlServer (8.0.11)
- Microsoft.EntityFrameworkCore.Tools (8.0.11)
- Microsoft.EntityFrameworkCore.Design (8.0.11) - *si se instala*

## Estructura del Proyecto

```
ApiEcommer/
├── Data/
│   └── ApplicationDbContext.cs
├── Models/
├── Controllers/
├── docker-compose.yaml
├── appsettings.json
└── Program.cs
```

## Notas Importantes

1. **Siempre usar `sa` en minúsculas** en la cadena de conexión
2. **Especificar el puerto 1433** en la cadena de conexión
3. **Eliminar volúmenes** si cambias la contraseña de SA
4. **Verificar que el contenedor esté corriendo** antes de ejecutar migraciones
5. **Las migraciones se crean automáticamente** basadas en los cambios en el DbContext