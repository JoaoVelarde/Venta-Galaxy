using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Interfaces
{
    public interface IVentaDetalleService
    {
        Task<PaginationResponse<VentaDetalleResponse>> ListAsync(BusquedaVentaRequest request,string cliente);
        Task<BaseResponse> AddMasivaAsync(string usuarioMail, VentaMasivaDtoRequest request);
        Task<BaseResponse> UpdateAsync(int id, VentaDtoRequest request);
        Task<BaseResponse> DeleteAsync(int id);
        //Task<BaseResponseGeneric<ICollection<TalleresPorMesDto>>> ReporteTalleresPorMes(int anio);
    }
}
