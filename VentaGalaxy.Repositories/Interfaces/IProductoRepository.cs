using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Entities;
using VentaGalaxy.Entities.Infos;

namespace VentaGalaxy.Repositories.Interfaces
{
    public interface IProductoRepository : IRepositoryBase<Producto>
    {
        Task<ICollection<Producto>> ListarAsync();

        Task<(ICollection<ProductoInfo> Collection, int Total)> ListarProductoAsync(string Producto, int? CategoriaId, int pagina, int filas);
        Task<ProductoHomeInfo?> ObtenerProductoHomeAsync(int id);
        Task<(ICollection<ProductoHomeInfo> Collection, int Total)> ListarProductosHomeAsync(string? Producto, int? CategoriaId, int pagina, int filas );
    }
}
