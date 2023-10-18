
using Vk.Base.Model;

namespace Vk.Data.Repository;

public interface IGenericRepository<TEntity> where TEntity : BaseModel
{ 
    Task<TEntity> GetById(int id ,CancellationToken cancellationToken, params string[] includes);
    Task<List<TEntity>> GetAll(CancellationToken cancellationToken ,params string[] includes);
    void Delete(int id);
    void DeleteAll();
    void Remove(int id);
    void Remove(TEntity entity);
    Task Update(int id, TEntity entity, CancellationToken cancellationToken);
    void Insert(TEntity entity,CancellationToken cancellationToken);
    void InsertRange(List<TEntity> entities,CancellationToken cancellationToken);

}