using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Repository;

namespace Vk.Data.Uow;

public class UnitOfWork : IUnitOfWork 
{
    private readonly VkDbContext dbContext;
    
    public UnitOfWork(VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        
        BookRepository = new GenericRepository<Book>(dbContext);
        AuthorRepository = new GenericRepository<Author>(dbContext);
        CategoryRepository = new GenericRepository<Category>(dbContext);
    }
    
    public void Complete()
    {
        dbContext.SaveChanges();
    }

    public void CompleteTransaction()
    {
        using (var transaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                dbContext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                //Log
            }
        }
    }
    
    public IGenericRepository<Book> BookRepository { get; }
    public IGenericRepository<Author> AuthorRepository { get; }
    public IGenericRepository<Category> CategoryRepository { get; }
}