using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using VentaGalaxy.DataAccess;
using VentaGalaxy.Entities;
using VentaGalaxy.Entities.Infos;
using VentaGalaxy.Repositories.Interfaces;

namespace VentaGalaxy.Repositories.Implementaciones
{
    public class VentaRepository(VentaGalaxyDbContext context) :
        RepositoryBase<Venta>(context), IVentaRepository
    {
        public async Task<Venta?> GetUltimaVentaAsync()
        {
            return await context.Set<Venta>()
                .OrderByDescending(v => v.Id)
                .FirstOrDefaultAsync();
        }
    }
}
