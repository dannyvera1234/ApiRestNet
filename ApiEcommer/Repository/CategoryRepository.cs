using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommer.Repository.IRepository;

namespace ApiEcommer.Repository
{
    // Implementación del patrón Repository para la entidad Category
    // Separa la lógica de acceso a datos del resto de la aplicación
    public class CategoryRepository : ICategoryRepository
    {
        // Campo privado para el contexto de base de datos
        // readonly significa que solo se puede asignar en el constructor
        private readonly ApplicationDbContext _db;

        // Constructor que recibe el contexto por inyección de dependencias
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // Verifica si existe una categoría por ID
        // Any() devuelve true si encuentra al menos un elemento que cumpla la condición
        public bool CategoryExists(int categoryId)
        {
            return _db.Categories.Any(c => c.Id == categoryId);
        }

        // Verifica si existe una categoría por nombre (sobrecarga del método)
        // ToLower().Trim() normaliza el texto para comparación case-insensitive
        public bool CategoryExists(string name)
        {
            return _db.Categories.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        // Crea una nueva categoría en la base de datos
        public bool CreateCategory(Category category)
        {
            // Establece la fecha de creación automáticamente
            category.CreationDate = DateTime.Now;
            // Add() marca la entidad para inserción
            _db.Categories.Add(category);
            // Guarda los cambios y retorna si fue exitoso
            return Save();
        }

        // Elimina una categoría de la base de datos
        public bool DeleteCategory(Category category)
        {
            // Remove() marca la entidad para eliminación
            _db.Categories.Remove(category);
            return Save();
        }

        // Actualiza una categoría existente
        public bool UpdateCategory(Category category)
        {
            // Establece la fecha de actualizacion automáticamente
            category.CreationDate = DateTime.Now;
            // Update() marca la entidad para actualización
            _db.Categories.Update(category);
            return Save();
        }

        // Obtiene todas las categorías ordenadas por nombre
        public ICollection<Category> GetCategories()
        {
            // OrderBy() ordena alfabéticamente, ToList() ejecuta la consulta
            return _db.Categories.OrderBy(c => c.Name).ToList();
        }

        // Obtiene una categoría específica por ID
        public Category GetCategory(int categoryId)
        {
            // FirstOrDefault() devuelve el primer elemento o null
            // ?? throw lanza excepción si no encuentra la categoría
            return _db.Categories.FirstOrDefault(c => c.Id == categoryId) ??
                   throw new InvalidOperationException($"La categoria {categoryId} no existe");
        }

        // Método privado para guardar cambios en la base de datos
        public bool Save()
        {
            // SaveChanges() retorna el número de entidades afectadas
            // > 0 significa que se guardó al menos un cambio
            return _db.SaveChanges() > 0;
        }
    }
}