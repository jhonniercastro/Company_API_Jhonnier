using Company_API_Jhonnier.Interfaces;
using Company_API_Jhonnier.Models;
using Company_API_Jhonnier.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company_API_Jhonnier.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProducts(AddProduct product)
    {
        var result = await _productsService.CreateProducts(product);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProducts(UpdateProduct product)
    {
        var result = await _productsService.UpdateProducts(product);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProducts(int idProduct)
    {
        var result = await _productsService.DeleteProducts(idProduct);
        return Ok(result);
    }
}
