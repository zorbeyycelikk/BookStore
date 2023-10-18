using Vk.Data.Domain;
using Vk.Data.Repository;

namespace Vk.Data.Uow;

public interface IUnitOfWork
{
    void Complete();
    void CompleteTransaction();
    
    IGenericRepository<Book> BookRepository { get; }
    IGenericRepository<Author> AuthorRepository { get; }
    IGenericRepository<Category> CategoryRepository { get; }
}