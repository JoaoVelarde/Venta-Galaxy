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
    public class VentaDetalleRepository(VentaGalaxyDbContext context) :
        RepositoryBase<VentaDetalle>(context), IVentaDetalleRepository
    {
        public async Task<(ICollection<VentaDetalleInfo> Collection, int Total)> ListarVentasAsync(int? ClienteId, string? NroVenta, string? Cliente, string? Producto, int pagina, int filas)
        {
            var tupla = await ListAsync(predicado: p =>
                    (NroVenta == null || p.Venta.NroVenta.Contains(NroVenta)) &&
                    (Cliente == null || p.Venta.Cliente.NombreCompleto.Contains(Cliente)) &&
                    (ClienteId == null || p.Venta.Cliente.Id == ClienteId) &&
                    (Producto == null || p.Producto.Nombre.Contains(Producto)),
                selector: p => new VentaDetalleInfo()
                {
                    Id = p.Id,
                    NroVenta = p.Venta.NroVenta,
                    Cliente = p.Venta.Cliente.NombreCompleto,
                    Producto = p.Producto.Nombre,
                    Cantidad = p.Cantidad,
                    Precio = p.Precio,
                    FechaCompra = p.FechaCrea.ToString("dd/MM/yyyy"),
                    Categoria = p.Producto.Categoria.Nombre
                },
                orderBy: x => x.Id,
                relaciones: "Producto.Categoria,Venta.Cliente",
                pagina, filas);

            return tupla;
        }

        public  async Task AddMasivaAsync(ICollection<VentaDetalle> ventaDetalle)
        {
            await Context.Set<VentaDetalle>().AddRangeAsync(ventaDetalle);
            await Context.SaveChangesAsync();
        }
    }
}
