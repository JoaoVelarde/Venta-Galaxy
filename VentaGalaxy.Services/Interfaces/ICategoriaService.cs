using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Shared.Request;
using VentaGalaxy.Shared.Response;

namespace VentaGalaxy.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<BaseResponseGeneric<ICollection<CategoriaDtoResponse>>> ListAsync();

        Task<BaseResponseGeneric<CategoriaDtoRequest>> FindByIdAsync(int id);

        Task<BaseResponse> AddAsync(CategoriaDtoRequest request);

        Task<BaseResponse> UpdateAsync(int id, CategoriaDtoRequest request);

        Task<BaseResponse> DeleteAsync(int id);
    }
}
