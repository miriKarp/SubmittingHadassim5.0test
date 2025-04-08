using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO;
using StoreManagement.Models;
using StoreManagement.Repo.IRepositories;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("GetProductById/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> AddProduct([FromBody] ProductDTO product)
    {
        await _productRepository.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProductById), new { }, product);
    }
}
