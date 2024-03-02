using Sclms.DTOS;
using Sclms.Persistence.Modles;
using Sclms.UseCases.IServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetProduct()
    {
        var product = await _productService.GetAllProductAsync();
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }


    [HttpGet("GetLookup")]
    public async Task<IActionResult> GetProductLookup()
    {
        var product = await _productService.GetProductLookup(); 
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
    {
        await _productService.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProduct), new { productId = product.Id }, product);
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProduct(long productId, [FromBody] Product product)
    {
        if (productId != product.Id)
        {
            return BadRequest();
        }

        await _productService.UpdateProductAsync(product);
        return NoContent();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(long productId)
    {
        await _productService.DeleteProductAsync(productId);
        return NoContent();
    }
}
