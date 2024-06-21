using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Entities;

namespace VentaGalaxy.Entities.Infos
{
    public class ClienteInfo
    {
        public int Id { get; set; }
        public string NroDocumento { get; set; } = default!;
        public string NombreCompleto { get; set; } = default!;
        public string Correo { get; set; } = default!;
        public string Telefono { get; set; } = default!;

    }
}
