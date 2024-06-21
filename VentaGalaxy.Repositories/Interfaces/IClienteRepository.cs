using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaGalaxy.Entities;
using VentaGalaxy.Entities.Infos;

namespace VentaGalaxy.Repositories.Interfaces
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<ICollection<Cliente>> ListarAsync();

        Task<(ICollection<ClienteInfo> Collection, int Total)> ListarClientesAsync(string? Cliente, int? ClienteId, string? NroDocumento, int pagina, int filas);
        Task<Cliente?> FindByEmailAsync(string email);
    }
}
