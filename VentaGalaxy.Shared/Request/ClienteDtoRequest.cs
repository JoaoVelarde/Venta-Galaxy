using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Shared.Request
{
    public class ClienteDtoRequest
    {
        public string NroDocumento { get; set; } = default!;
        public string NombreCompleto { get; set; } = default!;
        public string? Telefono { get; set; }
        public string Correo { get; set; } = default!;
    }
}
