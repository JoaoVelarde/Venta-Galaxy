using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Shared.Request
{
    public class BusquedaProductoRequest : RequestBase
    {
        public string? Producto { get; set; }
        public int? CategoriaId { get; set; }

    }
}
