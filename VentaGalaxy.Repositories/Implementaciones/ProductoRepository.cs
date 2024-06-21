using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.DataAccess;
using VentaGalaxy.Entities;
using VentaGalaxy.Entities.Infos;
using VentaGalaxy.Repositories.Interfaces;

namespace VentaGalaxy.Repositories.Implementaciones
{
    public class ProductoRepository(VentaGalaxyDbContext context) :  RepositoryBase<Producto>(context), IProductoRepository
    {
        public async Task<ICollection<Producto>> ListarAsync()
        {
            return await Context.Set<Producto>()
                .Include(p => p.Categoria)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(ICollection<ProductoInfo> Collection, int Total)> ListarProductoAsync(string? Producto, int? CategoriaId, int pagina, int filas)
        {
            try
            {

                var total = await Context.Set<Producto>()
                    .Where(p => (Producto == null || p.Nombre == Producto) &&
                                (CategoriaId == null || p.CategoriaId == CategoriaId))
                    .CountAsync();

                var tupla = await ListAsync(predicado: p =>
                        (Producto == null || p.Nombre == Producto) &&
                        (CategoriaId == null || p.CategoriaId == CategoriaId),
                    selector: p => new ProductoInfo()
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        PrecioCompra = p.PrecioCompra,
                        PrecioVenta = p.PrecioVenta,
                        Cantidad = p.Cantidad,
                        Url = p.Url,
                        Categoria = p.Categoria.Nombre //!= null ? p.Categoria.Nombre : null
                    },
                    orderBy: x => x.Id,
                    relaciones: "Categoria",
                    pagina,filas);

                return tupla;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<ProductoHomeInfo?> ObtenerProductoHomeAsync(int id)
        {
            return await Context.Set<Producto>()
                .Where(p => p.Id == id)
                .Select(p => new ProductoHomeInfo
                {
                    Id = p.Id,
                    Cantidad = p.Cantidad,
                    Categoria = p.Categoria.Nombre,
                    Descripcion = p.Descripcion,
                    PrecioVenta = p.PrecioVenta,
                    Producto = p.Nombre,
                    Url = p.Url
                })
                .FirstOrDefaultAsync();
        }

        public async Task<(ICollection<ProductoHomeInfo> Collection, int Total)> ListarProductosHomeAsync(string? Producto, int? CategoriaId, int pagina, int filas)
        {
            var tupla = await ListAsync(predicado: p => (Producto == null || p.Nombre.Contains(Producto))
                                                                 && (CategoriaId == null || p.CategoriaId == CategoriaId)
                                                                 && p.Cantidad > 0,
                selector: p => new ProductoHomeInfo
                {
                    Id = p.Id,
                    Cantidad = p.Cantidad,
                    Categoria = p.Categoria.Nombre,
                    Descripcion = p.Descripcion,
                    PrecioVenta = p.PrecioVenta,
                    Producto = p.Nombre,
                    Url = p.Url
                },
                orderBy: x => x.Id,
                relaciones: "Categoria",
                pagina: pagina,
                filas: filas);

            return tupla;
        }
    }
}
