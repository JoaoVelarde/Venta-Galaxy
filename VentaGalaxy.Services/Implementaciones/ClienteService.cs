using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Repositories.Interfaces;
using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Services.Utils;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Implementaciones
{
    public  class ClienteService : IClienteService
    {
        private readonly ILogger<ClienteService> _logger;
        private readonly IMapper _mapper;
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository, ILogger<ClienteService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<PaginationResponse<ClienteDtoResponse>> ListAsync(BusquedaClienteRequest request)
        {
            var response = new PaginationResponse<ClienteDtoResponse>();
            try
            { 
                var tupla = await _repository.ListarClientesAsync(request.Cliente, request.ClienteId, request.NroDocumento, request.Pagina, request.Filas);

                response.Data = _mapper.Map<ICollection<ClienteDtoResponse>>(tupla.Collection);
                response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar las vacaciones";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, ClienteDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await _repository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "No se encontró al cliente";
                    return response;
                }

                _mapper.Map(request, entity);

                await _repository.UpdateAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al actualizar al cliente";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

    }
}
