using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEcommer.Repository.IRepository;

namespace ApiEcommer.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool BuyProduct(string name, int quantity)
        {
            // Validación inicial: verifica que el nombre no esté vacío y la cantidad sea positiva
            if (string.IsNullOrEmpty(name) || quantity <= 0)
                return false;

            // Busca el producto en la base de datos por nombre (ignorando mayúsculas/minúsculas y espacios)
            var product = _db.Products.FirstOrDefault(p => p.Name.ToLower().Trim() == name.ToLower().Trim());

            // Si no encuentra el producto O si no hay suficiente stock, retorna false
            if (product == null || product.Stock < quantity)
                return false;

            // Reduce el stock del producto restando la cantidad comprada
            product.Stock -= quantity;

            // Actualiza el producto en la base de datos
            _db.Products.Update(product);

            // Guarda los cambios y retorna true si fue exitoso
            return Save();
        }
        public bool CreateProduct(Product product)
        {
            if (product == null)
                return false;

            if (ProductoExists(product.Name))
                return false;

            product.CreationDate = DateTime.Now;
            _db.Products.Add(product);
            return Save();
        }
        public bool DeleteProduct(Product product)
        {
            if (product == null)
                return false;

            _db.Products.Remove(product);
            return Save();
        }
        public Product? GetProduct(int id)
        {
            if (id <= 0)
                return null;

            return _db.Products.FirstOrDefault(c => c.ProductId == id);
        }
        public ICollection<Product> GetProducts()
        {
            return _db.Products.OrderBy(c => c.Name).ToList();
        }
        public ICollection<Product> GetProductsForCategory(int categoryId)
        {
            // Valida que el ID de categoría sea válido (debe ser mayor a 0)
            if (categoryId <= 0)
                return new List<Product>(); // Retorna lista vacía si el ID no es válido

            // Busca y retorna todos los productos que pertenecen a la categoría especificada
            return _db.Products.Where(c => c.CategoryId == categoryId).OrderBy(p => p.Name).ToList();
        }
        public bool ProductExists(int id)
        {
            if (id <= 0)
                return false;

            return _db.Products.Any(p => p.ProductId == id);
        }
        public bool ProductoExists(string name)
        {
            if (name == null || name == string.Empty)
                return false;

            return _db.Products.Any(p => p.Name.ToLower() == name.ToLower());
        }
        public ICollection<Product> SearchProduct(string name)
        {
            // IQueryable<Product> query = _db.Products; solo para usalor en multiple filtro
            // Valida que el nombre no esté vacío o nulo
            if (string.IsNullOrEmpty(name))
                return new List<Product>(); // Retorna lista vacía

            // Busca productos cuyo nombre CONTENGA el texto buscado (búsqueda parcial)
            // Ignora mayúsculas/minúsculas con ToLower()
            return _db.Products.Where(p => p.Name.ToLower().Contains(name.ToLower())).OrderBy(p => p.Name).ToList();
        }
        public bool UpdateProduct(Product product)
        {
            if (product == null)
                return false;

            product.UpdateDate = DateTime.Now;
            _db.Products.Update(product);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }
    }
}