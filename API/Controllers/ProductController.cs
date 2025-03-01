using Microsoft.AspNetCore.Mvc;
using shop_api.Application.Services;
using shop_api.Common;
using shop_api.Domain.Entities;
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

        if (result is ProblemDetailsResponse)
        {
            var problemDetails = (ProblemDetails)result.GetResult();
            return StatusCode((int)problemDetails.Status, problemDetails);
        }

        var products = (List<Product>)result.GetResult();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductByIdAsync(
        [FromRoute] int id)
    {
        var result = await _productService.GetProductByIdAsync(id);

        if (result is ProblemDetailsResponse)
        {
            var problemDetails = (ProblemDetails)result.GetResult();
            return StatusCode((int)problemDetails.Status, problemDetails);
        }

        var product = (Product)result.GetResult();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync(
        [FromBody] ProductViewModel product)
    {
        var result = await _productService.AddProductAsync(product);

        if (result is ProblemDetailsResponse)
        {
            var problemDetails = (ProblemDetails)result.GetResult();
            return StatusCode((int)problemDetails.Status, problemDetails);
        }

        var createdProduct = (Product)result.GetResult();
        return CreatedAtAction(nameof(GetProductByIdAsync), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductAsync(
        [FromRoute] int id, 
        [FromBody] ProductViewModel updated)
    {
        var result = await _productService.UpdateProductAsync(id, updated);

        if (result is ProblemDetailsResponse)
        {
            var problemDetails = (ProblemDetails)result.GetResult();
            return StatusCode((int)problemDetails.Status, problemDetails);
        }

        var updatedProduct = (Product)result.GetResult();
        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductAsync(
        [FromRoute] int id)
    {
        var result = await _productService.DeleteProductAsync(id);

        if (result is ProblemDetailsResponse)
        {
            var problemDetails = (ProblemDetails)result.GetResult();
            return StatusCode((int)problemDetails.Status, problemDetails);
        }

        var removedProduct = (Product)result.GetResult();
        return Ok(removedProduct);
    }
}