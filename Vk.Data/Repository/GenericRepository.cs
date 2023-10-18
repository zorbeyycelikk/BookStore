using Microsoft.EntityFrameworkCore;
using Vk.Base.Model;
using Vk.Data.Context;


namespace Vk.Data.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
{
    
    //Database temsilcisi
    private readonly VkDbContext dbContext;
    
    public GenericRepository(VkDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    // List All
    public async Task<List<TEntity>> GetAll(CancellationToken cancellationToken,params string[] includes)
    {
        var entity = dbContext.Set<TEntity>().AsQueryable();
        if (includes.Any())
        {
            entity = includes.Aggregate(entity, (current, incl) => current.Include(incl));
        }
        return await entity.ToListAsync(cancellationToken);
    }
    
    // List With Id
    public async Task<TEntity> GetById(int id,CancellationToken cancellationToken,params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();
        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, incl) => current.Include(incl));
        }
        return await query.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

        // return dbContext.Set<TEntity>().Find(id);
    }
    
    // Soft Delete All
    public void DeleteAll()
    {
        List<TEntity> entities = dbContext.Set<TEntity>().ToList();
        entities.ForEach(x =>
        {
            x.InsertDate = DateTime.UtcNow;
            x.IsActive = true;
        });
    }

    // Soft Delete With Id
    public void Delete(int id)
    {
        var entity = dbContext.Set<TEntity>().Find(id);
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        dbContext.Set<TEntity>().Update(entity);
    }

    // Hard Delete With Id
    public void Remove(int id)
    {
        var entity = dbContext.Set<TEntity>().Find(id);
        dbContext.Set<TEntity>().Remove(entity);
    }

    // Hard Delete 
    public void Remove(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    // Update With Entity
    public async Task Update(int id, TEntity entity, CancellationToken cancellationToken)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    // Insert Entity
    public void Insert(TEntity entity,CancellationToken cancellationToken)
    {
        entity.InsertDate = DateTime.Now;
        dbContext.Set<TEntity>().AddAsync(entity,cancellationToken); 
    }

    // Insert List
    public void InsertRange(List<TEntity> entities,CancellationToken cancellationToken)
    {
        entities.ForEach(x =>
        {
            x.InsertDate = DateTime.UtcNow; 
        });
        dbContext.Set<TEntity>().AddRangeAsync(entities,cancellationToken);
    }
}