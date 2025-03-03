using Microsoft.AspNetCore.Mvc;
using shop_api.Application.Services;
using shop_api.Domain.ViewModels;

namespace shop_api.API.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
        private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var result = await _productService.GetProductsAsync();

        return result.Match(
            p => Ok(p),
            err => BadRequest(err));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductByIdAsync(
        [FromRoute] int id)
    {
        var result = await _productService.GetProductByIdAsync(id);

        return result.Match(
            p => Ok(p),
            err => BadRequest(err));
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync(
        [FromBody] ProductViewModel product)
    {
        var result = await _productService.AddProductAsync(product);
        
        return result.Match(
            p => Ok(p),
            err => BadRequest(err));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductAsync(
        [FromRoute] int id, 
        [FromBody] ProductViewModel updated)
    {
        var result = await _productService.UpdateProductAsync(id, updated);

        return result.Match(
            p => Ok(p),
            err => BadRequest(err));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(
        [FromRoute] int id)
    {
        var result = await _productService.DeleteProductAsync(id);

        return result.Match(
            p => Ok(p),
            err => BadRequest(err));
    }
}