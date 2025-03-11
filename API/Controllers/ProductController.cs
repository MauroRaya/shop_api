using Microsoft.AspNetCore.Mvc;
using shop_api.Application.Services;
using shop_api.Domain.Entities;

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
        [FromBody] Product product)
    {
        var result = await _productService.AddProductAsync(product);
        
        return result.Match(
            p => Ok(p),
            err => BadRequest(err));
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateProductAsync(
        [FromBody] Product product)
    {
        var result = await _productService.UpdateProductAsync(product);

        return result.Match(
            p => Ok(p),
            err => BadRequest(err));
    }

    [HttpDelete()]
    public async Task<IActionResult> DeleteProductAsync(
        [FromBody] Product product)
    {
        var result = await _productService.DeleteProductAsync(product);

        return result.Match(
            p => Ok(p),
            err => BadRequest(err));
    }
}