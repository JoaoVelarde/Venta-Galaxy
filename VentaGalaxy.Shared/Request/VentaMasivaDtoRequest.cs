using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Shared.Request
{
    public class VentaMasivaDtoRequest 
    {
        public ICollection<VentaDetalleDtoRequest> Productos { get; set; } = new List<VentaDetalleDtoRequest>();
    }
}
