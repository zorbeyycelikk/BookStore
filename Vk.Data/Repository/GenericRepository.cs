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
    public List<TEntity> GetAll()
    {
       return dbContext.Set<TEntity>().AsNoTracking().ToList();
    }
    
    // List With Id
    public TEntity GetById(int id)
    {
        return dbContext.Set<TEntity>().Find(id);
    }
    
    // Soft Delete All
    public void Delete(TEntity entity)
    {
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        dbContext.Set<TEntity>().Update(entity); 
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
    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    // Insert Entity
    public void Insert(TEntity entity)
    {
        entity.InsertDate = DateTime.Now;
        dbContext.Set<TEntity>().Add(entity);    
    }

    // Insert List
    public void InsertRange(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.InsertDate = DateTime.UtcNow; 
        });
        dbContext.Set<TEntity>().AddRange(entities);
    }
}