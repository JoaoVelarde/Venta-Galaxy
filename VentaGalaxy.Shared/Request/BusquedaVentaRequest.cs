using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Shared.Request
{
    public class BusquedaVentaRequest : RequestBase
    {
        public string? NroVenta { get; set; }
        public string? Cliente { get; set; }
        public string? Producto { get; set; }
        public int? ClienteId { get; set; }
    }
}
