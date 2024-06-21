using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Entities
{
    public class VentaDetalle : EntityBase
    {
        public Venta Venta { get; set; } = default!;
        public int VentaId { get; set; }
        public Producto Producto { get; set; } = default!;
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
