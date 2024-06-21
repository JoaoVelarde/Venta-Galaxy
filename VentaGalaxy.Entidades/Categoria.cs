using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaGalaxy.Entities
{
    public  class Categoria :EntityBase
    {
        public string Nombre { get; set; } = default!;
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
