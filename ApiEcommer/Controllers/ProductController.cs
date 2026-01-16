using ApiEcommer.Models.Dtos;
using ApiEcommer.Models.Dtos.ProductDtos;
using ApiEcommer.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper) : ControllerBase
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetProducts()
    {
        var products = _productRepository.GetProducts();
        if (products.Count == 0)
            return NotFound("no hay productos disponible");

        var productsDto = _mapper.Map<List<ProductDto>>(products);
        return Ok(productsDto);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetProduct(int id)
    {
        if (id <= 0)
            return BadRequest(" el id no puede ser negativo.");

        if (!_productRepository.ProductExists(id))
            return BadRequest("no existe el producto");

        var product = _productRepository.GetProduct(id);
        if (product == null)
            return NotFound();

        var productDto = _mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult CreateProduct([FromBody] CreateProductDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest(ModelState);
        }

        if (_productRepository.ProductExists(productDto.Name))
        {
            ModelState.AddModelError("", "Product already exists");
            return StatusCode(409, ModelState);
        }

        if (!_categoryRepository.CategoryExists(productDto.CategoryId))
        {
            ModelState.AddModelError("", "Category does not exist");
            return BadRequest(ModelState);
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var product = _mapper.Map<Product>(productDto);
        _productRepository.CreateProduct(product);
        var productDtoResponse = _mapper.Map<ProductDto>(product);
        return CreatedAtRoute("GetProduct", new { id = product.ProductId }, productDtoResponse);
    }





    [HttpPatch("{id:int}", Name = "UpdateProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]

    public IActionResult UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
    {
        if (updateProductDto == null)
            return BadRequest("los datos son requeridos");

        if (!ModelState.IsValid)
            return Conflict(ModelState);

        var existeProduct = _productRepository.GetProduct(id);

        if (existeProduct == null)
            return NotFound("no existe el producto");

        if (existeProduct.Name != updateProductDto.Name && _productRepository.ProductExists(updateProductDto.Name))
        {

            ModelState.AddModelError("", "el producto ya existe");
            return Conflict(ModelState);

        }

        _mapper.Map(updateProductDto, existeProduct);

        _productRepository.UpdateProduct(existeProduct);

        var responseDto = _mapper.Map<ProductDto>(existeProduct);

        return Ok(responseDto);

    }
    [HttpDelete("{id:int}", Name = "DeleteProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteProduct(int id)
    {
        if (!_productRepository.ProductExists(id))
            return NotFound("no existe el producto para eliminar");

        var existeProduct = _productRepository.GetProduct(id);

        if (existeProduct == null)
            return NotFound("no existe el producto");

        _productRepository.DeleteProduct(existeProduct);

        return Ok($"La categoria {id} fue eliminada");
    }

    [HttpGet("ByProductsForCategory/{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetProductsForCategory(int categoryId)
    {
        if (categoryId >= 0 && !_categoryRepository.CategoryExists(categoryId))
            return NotFound("no existe la categoria");

        var products = _productRepository.GetProductsForCategory(categoryId);
        if (products == null)
            return NotFound("no existen productos para esta categoria");

        var productsDto = _mapper.Map<List<ProductDto>>(products);
        return Ok(productsDto);
    }
    [HttpGet("BySearchProduct/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetSearchProduct(string name)
    {
        if (string.IsNullOrEmpty(name))
            return BadRequest("el nombre es requerido");

        var products = _productRepository.SearchProduct(name);
        if (products.Count == 0)
            return NotFound("no existen productos con ese nombre");

        var productsDto = _mapper.Map<List<ProductDto>>(products);
        return Ok(productsDto);
    }
}
