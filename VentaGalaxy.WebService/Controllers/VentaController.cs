using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Shared;
using VentaGalaxy.Shared.Request;

namespace VentaGalaxy.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaDetalleService _service;
        private readonly ILogger<VentaController> _logger;

        public VentaController(IVentaDetalleService serviceDetalle, ILogger<VentaController> logger)
        {
            _service = serviceDetalle;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BusquedaVentaRequest request)
        {
            var cliente = User.Claims.First(p => p.Type == ClaimTypes.Email).Value;

            var response = await _service.ListAsync(request,cliente);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost("masiva")]
        [Authorize(Roles = Constantes.RolCliente)]
        public async Task<IActionResult> PostMasiva([FromBody] VentaMasivaDtoRequest request)
        {

            var usuario = User.Claims.First(p => p.Type == ClaimTypes.Email).Value;

            _logger.LogInformation(usuario);

            var response = await _service.AddMasivaAsync(usuario, request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Constantes.RolAdministrador)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteAsync(id);

            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
