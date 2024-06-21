using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Shared.Response
{
    public class VentasPorMesDtoResponse
    {
        public string Mes { get; set; } = default!;
        public string Producto { get; set; } = default!;
        public int Cantidad { get; set; }
    }
}
