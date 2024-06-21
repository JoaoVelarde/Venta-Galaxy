using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Entities;
using VentaGalaxy.Entities.Infos;

namespace VentaGalaxy.Repositories.Interfaces
{
    public interface IVentaRepository : IRepositoryBase<Venta>
    {
        Task<Venta?> GetUltimaVentaAsync();
    }
}
