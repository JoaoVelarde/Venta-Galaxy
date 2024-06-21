using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Entities.Infos
{
    public class VentaDetalleInfo
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = default!;
        public string NroVenta { get; set; } = default!;
        public string Producto { get; set; } = default!;
        public string Categoria { get; set; } = default!;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string FechaCompra { get; set; } = default!;
    }
}
