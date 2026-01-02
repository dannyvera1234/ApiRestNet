using System;
using ApiEcommer.Models.Dtos;  // DTOs para transferir datos
using ApiEcommer.Repository.IRepository;  // Interfaz del repositorio
using ApiEcommer.Models;  // Entidades del modelo
using Microsoft.AspNetCore.Mvc;  // Controladores de ASP.NET Core
using AutoMapper;  // Para mapeo automático entre objetos

namespace ApiEcommer.Controllers;

// [ApiController] - Habilita características automáticas de API
[ApiController]
// [Route] - Define la ruta base: "api/Category"
[Route("api/[controller]")]
// Constructor principal de C# 12 - inyecta dependencias automáticamente
public class CategoryController(ICategoryRepository categoryRepository, IMapper mapper) : ControllerBase
{
    // Campos readonly - solo se asignan en el constructor
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;  // AutoMapper para conversiones

    // GET: api/Category - Obtiene todas las categorías
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]  // Documenta respuesta exitosa
    public IActionResult GetCategories()
    {
        // 1. Obtiene todas las categorías desde la base de datos
        var categories = _categoryRepository.GetCategories();

        // 2. AutoMapper convierte List<Category> a List<CategoryDto>
        var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

        // 3. Devuelve HTTP 200 OK con la lista de DTOs
        return Ok(categoriesDto);
    }

    // GET: api/Category/{id} - Obtiene una categoría específica
    [HttpGet("{categoryId:int}", Name = "GetCategory")]  // {id:int} = solo acepta números
    [ProducesResponseType(StatusCodes.Status200OK)]      // 200: Encontrada
    [ProducesResponseType(StatusCodes.Status404NotFound)] // 404: No encontrada
    public IActionResult GetCategory(int categoryId)
    {
        // 1. Busca la categoría por ID
        var category = _categoryRepository.GetCategory(categoryId);

        // 2. Si no existe, devuelve 404 Not Found
        if (category == null)
            return NotFound($"La categoria {categoryId} no existe");

        // 3. AutoMapper convierte Category a CategoryDto
        var categoryDto = _mapper.Map<CategoryDto>(category);

        // 4. Devuelve HTTP 200 OK con el DTO
        return Ok(categoryDto);
    }

    // POST: api/Category - Crea una nueva categoría
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]    // 201: Creada exitosamente
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // 400: Datos inválidos
    [ProducesResponseType(StatusCodes.Status409Conflict)]   // 409: Ya existe
    public IActionResult CreateCategory([FromBody] CreateCategoryDto categoryDto)
    {
        // 1. Validar que el objeto no sea null
        if (categoryDto == null)
            return BadRequest(ModelState);

        // 2. Verificar si ya existe una categoría con el mismo nombre
        if (_categoryRepository.CategoryExists(categoryDto.Name))
        {
            ModelState.AddModelError("Error", "La categoria ya existe");
            return Conflict(ModelState);  // HTTP 409 Conflict
        }

        // 3. Validar que todos los datos cumplan las reglas del DTO
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // 4. AutoMapper convierte CreateCategoryDto a Category
        // (El perfil asigna automáticamente CreationDate = DateTime.UtcNow)
        var category = _mapper.Map<Category>(categoryDto);

        // 5. Guarda en la base de datos (EF asigna el ID automáticamente)
        _categoryRepository.CreateCategory(category);

        // 6. AutoMapper convierte la entidad guardada a CategoryDto
        var responseDto = _mapper.Map<CategoryDto>(category);

        // 7. Devuelve HTTP 201 Created con la ubicación del nuevo recurso
        return CreatedAtRoute("GetCategory", new { categoryId = category.Id }, responseDto);
    }

    // PATCH: api/Category/{id} - Actualiza una categoría existente
    [HttpPatch("{id:int}", Name = "UpdateCategory")]       // PATCH = actualización parcial
    [ProducesResponseType(StatusCodes.Status200OK)]         // 200: Actualizada
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // 400: Datos inválidos
    [ProducesResponseType(StatusCodes.Status404NotFound)]   // 404: No existe
    [ProducesResponseType(StatusCodes.Status409Conflict)]   // 409: Nombre duplicado
    public IActionResult UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
    {
        // 1. Validar que el DTO no sea null
        if (updateCategoryDto == null)
            return BadRequest("Los datos de la categoría son requeridos");

        // 2. Validar que los datos cumplan las reglas del DTO
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // 3. Buscar la categoría existente por ID
        var existingCategory = _categoryRepository.GetCategory(id);
        if (existingCategory == null)
            return NotFound($"La categoria {id} no existe");

        // 4. Verificar si el nuevo nombre ya existe (solo si cambió)
        if (existingCategory.Name != updateCategoryDto.Name &&
            _categoryRepository.CategoryExists(updateCategoryDto.Name))
        {
            ModelState.AddModelError("Error", "Ya existe una categoría con ese nombre");
            return Conflict(ModelState);
        }

        // 5. AutoMapper actualiza la entidad existente con los nuevos datos
        // (El perfil ignora Id y CreationDate automáticamente)
        _mapper.Map(updateCategoryDto, existingCategory);

        // 6. Guarda los cambios en la base de datos
        _categoryRepository.UpdateCategory(existingCategory);

        // 7. AutoMapper convierte la entidad actualizada a DTO
        var responseDto = _mapper.Map<CategoryDto>(existingCategory);

        // 8. Devuelve HTTP 200 OK con la categoría actualizada
        return Ok(responseDto);
    }

    [HttpDelete("{id:int}", Name = "DeleteCategory")]  // DELETE = eliminar
    [ProducesResponseType(StatusCodes.Status200OK)]         // 200: Actualizada
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // 400: Datos inválidos
    [ProducesResponseType(StatusCodes.Status404NotFound)]   // 404: No existe

    public IActionResult DeleteCategory(int id)
    {
        // 1. Buscar la categoría existente por ID
        if (_categoryRepository.CategoryExists(id))
        {
            return NotFound($"La categoria {id} no existe");
        }

        var existingCategory = _categoryRepository.GetCategory(id);
        if (existingCategory == null)
        {
            return NotFound($"La categoria {id} no existe");
        }
        // 2. Eliminar la categoría de la base de datos
        _categoryRepository.DeleteCategory(existingCategory);

        // 3. Devuelve HTTP 200 OK
        return Ok($"La categoria {id} fue eliminada");
    }
}