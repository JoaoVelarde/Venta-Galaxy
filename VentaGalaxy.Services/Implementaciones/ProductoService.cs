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
using VentaGalaxy.Services.Utils;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Implementaciones
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        private readonly ILogger<ProductoService> _logger;
        private readonly IMapper _mapper;

        public ProductoService(IProductoRepository repository, ILogger<ProductoService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PaginationResponse<ProductoDtoResponse>> ListAsync(BusquedaProductoRequest request)
        {
            var response = new PaginationResponse<ProductoDtoResponse>();
            try
            {
                // string? Producto, int? CategoriaId, int pagina, int filas
                var tupla = await _repository.ListarProductoAsync(request.Producto, request.CategoriaId, request.Pagina, request.Filas);

                response.Data = _mapper.Map<ICollection<ProductoDtoResponse>>(tupla.Collection);
                response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los productos";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> AddAsync(ProductoDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var entity = _mapper.Map<Producto>(request);
                await _repository.AddAsync(entity);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al agregar el producto";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, ProductoDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await _repository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "No se encontró el producto";
                    return response;
                }

                _mapper.Map(request, entity);

                await _repository.UpdateAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al actualizar el producto";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await _repository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "No se encontró el producto";
                    return response;
                }

                await _repository.DeleteAsync(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al eliminar el producto";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponseGeneric<ProductoDtoRequest>> FindByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<ProductoDtoRequest>();
            try
            {
                var entity = await _repository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "Categoria no encontrada";
                    return response;
                }

                response.Data = _mapper.Map<ProductoDtoRequest>(entity);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar el producto por ID";
                _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<PaginationResponse<ProductoHomeDtoResponse>> ListarProductosHomeAsync(BusquedaProductoHomeRequest request)
        {
            var response = new PaginationResponse<ProductoHomeDtoResponse>();

            try
            {
                var tupla = await _repository.ListarProductosHomeAsync(request.Nombre,request.CategoriaId , request.Pagina, request.Filas);

                response.Data = _mapper.Map<ICollection<ProductoHomeDtoResponse>>(tupla.Collection);
                response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar los productos";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponseGeneric<ProductoHomeDtoResponse>> GetProductoHomeAsync(int id)
        {
            var response = new BaseResponseGeneric<ProductoHomeDtoResponse>();
            try
            {
                var entity = await _repository.ObtenerProductoHomeAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "No se encontró el Producto";
                    return response;
                }

                response.Data = _mapper.Map<ProductoHomeDtoResponse>(entity);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al buscar el Producto";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
