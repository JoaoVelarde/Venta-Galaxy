using Microsoft.EntityFrameworkCore;
using VentaGalaxy.DataAccess;
using VentaGalaxy.Entities;
using System.Linq.Expressions;
using VentaGalaxy.Repositories.Interfaces;

namespace VentaGalaxy.Repositories.Implementaciones;

public class RepositoryBase<TEntity>(VentaGalaxyDbContext context) : IRepositoryBase<TEntity>
    where TEntity : EntityBase
{
    protected readonly VentaGalaxyDbContext Context = context;

    public async Task<ICollection<TEntity>> ListAsync()
    {
        return await Context.Set<TEntity>()
            .AsNoTracking() 
            .ToListAsync();
    }

    public async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>()
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<TInfo>> ListAsync<TInfo>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TInfo>> selector,
        string? relaciones = null)
    {
        var collection = Context.Set<TEntity>()
            .Where(predicate)
            .AsQueryable();

        if (!string.IsNullOrEmpty(relaciones))
        {
            foreach (var tabla in relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                collection = collection.Include(tabla);
            }
        }

        return await collection
            .AsNoTracking()
            .Select(selector)
            .ToListAsync();
    }

    public async Task<(ICollection<TInfo> Collection, int Total)> ListAsync<TInfo, TKey>(
        Expression<Func<TEntity, bool>> predicado,
        Expression<Func<TEntity, TInfo>> selector,
        Expression<Func<TEntity, TKey>> orderBy,
        string? relaciones = null,
        int pagina = 1, int filas = 5)
    {
        var query = Context.Set<TEntity>()
            .Where(predicado)
            .AsQueryable();

        if (!string.IsNullOrEmpty(relaciones))
        {
            foreach (var tabla in relaciones.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(tabla);
            }
        }

        var collection = await query
            .OrderBy(orderBy)
            .Skip((pagina - 1) * filas)
            .Take(filas)
            .Select(selector)
            .ToListAsync();

        var total = await Context.Set<TEntity>()
            .Where(predicado)
            .CountAsync();

        return (collection, total);
    }

    public async Task<TEntity?> FindByIdAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var registro = await FindByIdAsync(id);
        if (registro is not null)
        {
            registro.Estado = false;
            await UpdateAsync();
        }
    }
}