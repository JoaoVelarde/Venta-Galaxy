using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Entities.Infos
{
    public class ProductoInfo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int? Cantidad { get; set; }
        public string Categoria { get; set; } = default!;
        public string Url { get; set; } = default!;
    }
}
