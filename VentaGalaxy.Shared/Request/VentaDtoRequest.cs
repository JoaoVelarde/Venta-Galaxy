using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Shared.Request
{
    public class VentaDtoRequest
    {
        public int ClienteId { get; set; }
        public string NroVenta { get; set; }
        public List<VentaDetalleDtoRequest> VentaDetalle = new List<VentaDetalleDtoRequest>();
    }
}
