using Microsoft.AspNetCore.Mvc;
using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Shared.Request;

namespace VentaGalaxy.WebService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IProductoService _productoService;

    public HomeController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] BusquedaProductoHomeRequest request)
    {
        if (request.Pagina <= 0)
            request.Pagina = 1;
        if (request.Filas <= 0)
            request.Filas = 5;

        var response = await _productoService.ListarProductosHomeAsync(request);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _productoService.GetProductoHomeAsync(id);
        return Ok(response);
    }
}