using Microsoft.EntityFrameworkCore;
using Zademy.Persistence.Data;

namespace Zademy.Persistence.Repositories;

public abstract class BaseRepository<TEntity> where TEntity : class
{
    protected readonly ZademyAppDbContext Context;
    private readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(ZademyAppDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<TEntity>();
    }

    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return _dbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual Task<TEntity?> GetByIdAsync(int id)
    {
        return _dbSet.FindAsync(id).AsTask();
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity?> UpdateAsync(int id, TEntity entity)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity is null)
            return null;

        Context.Entry(existingEntity).CurrentValues.SetValues(entity);

        await Context.SaveChangesAsync();
        return existingEntity;
    }


    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null)
            return false;

        _dbSet.Remove(entity);
        await Context.SaveChangesAsync();
        return true;
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.FindAsync(id) != null;
    }
}