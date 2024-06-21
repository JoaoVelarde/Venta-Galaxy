using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Entities
{
    public class Venta : EntityBase
    {
        public Cliente Cliente { get; set; } = default!;
        public int ClienteId { get; set; }
        public string NroVenta { get; set; }
        public ICollection<VentaDetalle> VentaDetalle { get; set; } = new List<VentaDetalle>();
    }
}
