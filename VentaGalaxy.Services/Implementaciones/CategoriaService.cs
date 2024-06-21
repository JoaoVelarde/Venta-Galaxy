using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Entities;
using VentaGalaxy.Repositories.Interfaces;
using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Implementaciones
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ILogger<CategoriaService> _logger;
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _repository;
        public CategoriaService(ICategoriaRepository repository, ILogger<CategoriaService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<BaseResponse> AddAsync(CategoriaDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.AddAsync(_mapper.Map<Categoria>(request));
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al agregar la categoria";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar la categoria";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<CategoriaDtoRequest>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<CategoriaDtoRequest>();
            try
            {
                var categoria = await _repository.FindByIdAsync(id);
                if (categoria == null)
                {
                    response.ErrorMessage = "Categoria no encontrada";
                    return response;
                }

                response.Data = _mapper.Map<CategoriaDtoRequest>(categoria);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar la categoria por ID";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ICollection<CategoriaDtoResponse>>> ListAsync()
        {
            var response = new BaseResponseGeneric<ICollection<CategoriaDtoResponse>>();
            try
            {
                var collection = await _repository.ListAsync();
                response.Data = _mapper.Map<ICollection<CategoriaDtoResponse>>(collection);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error en los categorias";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, CategoriaDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var cargo = await _repository.FindByIdAsync(id);
                if (cargo == null)
                {
                    response.ErrorMessage = "Categoria no encontrado";
                    return response;
                }

                _mapper.Map(request, cargo);

                await _repository.UpdateAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al actualizar la categoria";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }
    }
}
