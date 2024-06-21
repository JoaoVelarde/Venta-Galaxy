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
    public class ClienteRepository(VentaGalaxyDbContext context) : RepositoryBase<Cliente>(context), IClienteRepository
    {
        public async Task<ICollection<Cliente>> ListarAsync()
        {
            return await Context.Set<Cliente>()
                .Include(p => p.Ventas)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<(ICollection<ClienteInfo> Collection, int Total)> ListarClientesAsync(string? Cliente, int? ClienteId, string? NroDocumento, int pagina, int filas)
        {
            await using var multipleQuery = await Context.Database.GetDbConnection()
                .QueryMultipleAsync(
                    sql: "uspListarClientes",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        Cliente,
                        ClienteId = ClienteId == 0 ? (int?)null : ClienteId,
                        NroDocumento,
                        pagina = pagina - 1,
                        filas
                    });

            try
            {
                var collection = multipleQuery.Read<ClienteInfo>().ToList();
                var total = multipleQuery.ReadFirst<int>();

                return (collection, total);
            }
            catch (Exception ex)
            {
                return (new List<ClienteInfo>(), 0);
            }
        }

        public async Task<Cliente?> FindByEmailAsync(string email)
        {
            return await Context.Set<Cliente>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Correo == email);
        }
    }
}
