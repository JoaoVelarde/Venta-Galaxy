using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Entities;
using VentaGalaxy.Entities.Infos;

namespace VentaGalaxy.Repositories.Interfaces
{
    public interface IVentaDetalleRepository : IRepositoryBase<VentaDetalle>
    {
        Task<(ICollection<VentaDetalleInfo> Collection, int Total)> ListarVentasAsync(int? ClienteId, string? NroVenta, string? Cliente, string? Producto, int pagina, int filas);
        Task AddMasivaAsync(ICollection<VentaDetalle> ventaDetalle);
    }
}
