using Microsoft.AspNetCore.Mvc;
using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Shared.Request;

namespace VentaGalaxy.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BusquedaClienteRequest request)
        {
            var response = await _service.ListAsync(request);

            return response.Success ? Ok(response) : BadRequest(response);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ClienteDtoRequest request)
        {
            var response = await _service.UpdateAsync(id, request);

            return response.Success ? Ok(response) : BadRequest(response);
        }

    }
}
