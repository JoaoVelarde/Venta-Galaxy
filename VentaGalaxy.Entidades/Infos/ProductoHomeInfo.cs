using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Entities.Infos
{
    public class ProductoHomeInfo
    {
        public int Id { get; set; }
        public string? Producto { get; set; }
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public string? Url { get; set; }
    }
}
