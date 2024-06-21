using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Shared.Request
{
    public  class BusquedaClienteRequest : RequestBase
    {
        public string? Cliente { get; set; }
        public int? ClienteId { get; set; }
        public string? NroDocumento { get; set; }
    }
}
