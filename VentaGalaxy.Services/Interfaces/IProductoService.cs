using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Interfaces
{
    public interface IProductoService
    {
        Task<PaginationResponse<ProductoDtoResponse>> ListAsync(BusquedaProductoRequest request);
        Task<BaseResponse> AddAsync(ProductoDtoRequest request);
        Task<BaseResponse> UpdateAsync(int id, ProductoDtoRequest request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponseGeneric<ProductoDtoRequest>> FindByIdAsync(int id);
        Task<PaginationResponse<ProductoHomeDtoResponse>> ListarProductosHomeAsync(BusquedaProductoHomeRequest request);
        Task<BaseResponseGeneric<ProductoHomeDtoResponse>> GetProductoHomeAsync(int id);
    }
}
