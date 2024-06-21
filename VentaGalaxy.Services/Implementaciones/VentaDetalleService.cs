using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.DataAccess;
using VentaGalaxy.Entities;
using VentaGalaxy.Repositories.Interfaces;
using VentaGalaxy.Services.Interfaces;
using VentaGalaxy.Services.Utils;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Implementaciones
{
    public class VentaDetalleService : IVentaDetalleService
    {
        private readonly IVentaDetalleRepository _repository;
        private readonly IVentaRepository _repositoryVenta;
        private readonly IProductoRepository _repositoryProducto;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProductoService _productoService;
        private readonly ILogger<VentaDetalleService> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ClienteIdentityUser> _userManager;

        public VentaDetalleService(UserManager<ClienteIdentityUser> userManager, IVentaDetalleRepository repository, IVentaRepository repositoryVenta, IProductoService iProductoService, IProductoRepository repositoryProducto, IClienteRepository clienteRepository, ILogger<VentaDetalleService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _repositoryProducto = repositoryProducto;
            _repositoryVenta = repositoryVenta;
            _productoService = iProductoService;
            _userManager = userManager;
        }


        public async Task<PaginationResponse<VentaDetalleResponse>> ListAsync(BusquedaVentaRequest request, string cliente)
        {
            var response = new PaginationResponse<VentaDetalleResponse>();
            try
            {
                //verificamos si puede buscar como admin
                ClienteIdentityUser? user = null;
                user = await _userManager.FindByEmailAsync(cliente);
                var roles = await _userManager.GetRolesAsync(user);
                var isAdmin = roles.Contains("Administrador");

                if (!isAdmin)
                {
                    var clienteUser = await _clienteRepository.FindByEmailAsync(cliente);
                    if (clienteUser is null)
                    {
                        response.ErrorMessage = "El cliente no existe";
                        return response;
                    }

                    request.ClienteId = clienteUser.Id;
                }
                else
                {
                    request.ClienteId = null;
                }

                var tupla = await _repository.ListarVentasAsync(request.ClienteId, request.NroVenta, request.Cliente, request.Producto, request.Pagina, request.Filas);

                response.Data = _mapper.Map<ICollection<VentaDetalleResponse>>(tupla.Collection);
                response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al listar las Ventas";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> AddMasivaAsync( string usuarioMail, VentaMasivaDtoRequest request)
        {

            var response = new BaseResponse();

            try
            {
              
                var lstVentadetalle = new List<VentaDetalle>();

                var cliente = await _clienteRepository.FindByEmailAsync(usuarioMail);
                if (cliente is null)
                {
                    response.ErrorMessage = "El cliente no existe";
                    return response;
                }

                var ultimaVenta = await _repositoryVenta.GetUltimaVentaAsync();

                string nroVenta = GenerarCorrelativoSiguiente(ultimaVenta?.NroVenta);

                var venta = new Venta
                {
                    NroVenta = nroVenta,
                    ClienteId = cliente.Id
                };

                var entity = _mapper.Map<Venta>(venta);

                await _repositoryVenta.AddAsync(entity);

                int idGenerado = venta.Id;

                foreach (var detalle in request.Productos)
                {

                    var ventaDetalle = new VentaDetalle
                    {
                        Cantidad = detalle.Cantidad,
                        Precio = detalle.Precio,
                        ProductoId = detalle.ProductoId,
                        VentaId = idGenerado
                    };

                    var registro = await _repositoryProducto.FindByIdAsync(detalle.ProductoId);

                    ProductoDtoRequest prod = new ProductoDtoRequest();

                    prod.Cantidad = registro.Cantidad - detalle.Cantidad;
                    prod.Nombre =registro.Nombre;
                    prod.Descripcion =registro.Descripcion;
                    prod.Id = registro.Id;
                    prod.PrecioCompra = registro.PrecioCompra;
                    prod.PrecioVenta = registro.PrecioVenta;
                    prod.CategoriaId = registro.CategoriaId;
                    prod.Url = registro.Url;

                    await _productoService.UpdateAsync(registro.Id, prod);

                    lstVentadetalle.Add(ventaDetalle);
                }

                await _repository.AddMasivaAsync(lstVentadetalle);

                response.ErrorMessage = "Su compra fue generado con el Nro: " + nroVenta;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al agregar la venta";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;

        }

        public async Task<BaseResponse> UpdateAsync(int id, VentaDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var entity = await _repository.FindByIdAsync(id);
                if (entity == null)
                {
                    response.ErrorMessage = "No se encontró la vacación";
                    return response;
                }

                var newVentaDetalle = _mapper.Map<VentaDetalle>(request);

                entity.Cantidad = newVentaDetalle.Cantidad;
                entity.Precio = newVentaDetalle.Precio;
                entity.ProductoId = newVentaDetalle.ProductoId;

                await _repository.UpdateAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al actualizar la vacación";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
                var response = new BaseResponse();
                try
                {

                    var entityDet = await _repository.FindByIdAsync(id);

                    if (entityDet == null)
                    {
                        response.ErrorMessage = "No se encontró el detalle de la venta";
                        return response;
                    }

                    var registro = await _repositoryProducto.FindByIdAsync(entityDet.ProductoId);

                    ProductoDtoRequest prod = new ProductoDtoRequest();

                    prod.Cantidad = registro.Cantidad + entityDet.Cantidad;
                    prod.Nombre = registro.Nombre;
                    prod.Descripcion = registro.Descripcion;
                    prod.Id = registro.Id;
                    prod.PrecioCompra = registro.PrecioCompra;
                    prod.PrecioVenta = registro.PrecioVenta;
                    prod.CategoriaId = registro.CategoriaId;
                    prod.Url = registro.Url;

                    //producto
                    await _productoService.UpdateAsync(registro.Id, prod);
                    //detalle venta
                    await _repository.DeleteAsync(entityDet.Id);
                    //venta
                    await _repositoryVenta.DeleteAsync(entityDet.VentaId);

                    response.Success = true;

                }
                catch (Exception ex)
                {
                    response.ErrorMessage = "Error al eliminar la venta";
                    _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
                }
                return response;
        }

        private string GenerarCorrelativoSiguiente(string? ultimoNumero)
        {
            if (string.IsNullOrEmpty(ultimoNumero))
            {
                return "B001-20240001";
            }

            string correlativoActualStr = ultimoNumero.Substring(ultimoNumero.Length - 8);
            if (int.TryParse(correlativoActualStr, out int correlativoActual))
            {
                int siguienteCorrelativo = correlativoActual + 1;
                return $"B001-{siguienteCorrelativo:D8}";
            }
            else
            {
                throw new InvalidOperationException("El formato del número de venta es incorrecto.");
            }
        }

    }
}
