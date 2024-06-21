using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Interfaces
{
    public interface IClienteService
    {
        Task<PaginationResponse<ClienteDtoResponse>> ListAsync(BusquedaClienteRequest request);
        Task<BaseResponse> UpdateAsync(int id, ClienteDtoRequest request);
    
    }
}
