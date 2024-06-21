using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.DataAccess;
using VentaGalaxy.Entities;
using VentaGalaxy.Repositories.Interfaces;

namespace VentaGalaxy.Repositories.Implementaciones
{
    public class CategoriaRepository(VentaGalaxyDbContext context) : RepositoryBase<Categoria>(context), ICategoriaRepository
    {
    }
}
