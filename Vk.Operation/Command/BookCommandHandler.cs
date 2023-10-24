using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;
using InvalidOperationException = System.InvalidOperationException;

namespace Vk.Operation.Command;

public class  BookCommandHandler :
    IRequestHandler<CreateBookCommand, ApiResponse<BookResponse>>,
    IRequestHandler<DeleteBookCommand, ApiResponse>,
    IRequestHandler<DeleteBookAllCommand, ApiResponse>,
    IRequestHandler<HardDeleteBookCommand, ApiResponse>,
    IRequestHandler<HardDeleteBookAllCommand, ApiResponse>,
    IRequestHandler<UpdateBookCommand, ApiResponse>
{
    // private readonly VkDbContext dbContext;
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    public BookCommandHandler(IMapper mapper,VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    public async Task<ApiResponse<BookResponse>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        Book result = dbContext.Set<Book>().SingleOrDefault(x => x.BookNumber == request.Model.BookNumber);
        
        if (result is not null)
        {
            return new ApiResponse<BookResponse>("Error");
        }
        
        Book mapped = mapper.Map<Book>(request.Model);
        mapped.InsertDate = DateTime.UtcNow;
        
        var entity = await dbContext.Set<Book>().AddAsync(mapped, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        BookResponse response = mapper.Map<BookResponse>(entity.Entity);
        return new ApiResponse<BookResponse>(response); // true , "Success"
    }
    
    
    public async Task<ApiResponse> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        Book entity = await dbContext.Set<Book>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null)
        {
            return new ApiResponse("Error");
        }
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow; 
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }

    public async Task<ApiResponse> Handle(DeleteBookAllCommand request, CancellationToken cancellationToken)
    {
        List<Book> entities = await dbContext.Set<Book>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .ToListAsync(cancellationToken);

        if (entities == null)
        {
            return new ApiResponse("Error");        }
        
        entities.ForEach(x =>
        {
            x.InsertDate = DateTime.UtcNow;
            x.IsActive = false;
        });
        dbContext.Set<Book>().UpdateRange(entities);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }

    public async Task<ApiResponse> Handle(HardDeleteBookCommand request, CancellationToken cancellationToken)
    {
        Book entity = await dbContext.Set<Book>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null)
        {
            return new ApiResponse("Error");
        }
        dbContext.Set<Book>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(HardDeleteBookAllCommand request, CancellationToken cancellationToken)
    {
        List<Book> entities = await dbContext.Set<Book>().Include(x => x.Author)
            .Include(x => x.Category)
            .ToListAsync();
        if (entities == null)
        {
            return new ApiResponse("Error");
        }
        
        entities.ForEach(x =>
        {
            dbContext.Set<Book>().Remove(x);

        });
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Book>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Error");
        }
        entity.BookNumber = request.Model.BookNumber;
        entity.PageCount = request.Model.PageCount;
        entity.Publisher = request.Model.Publisher;
        entity.UpdateDate = DateTime.UtcNow;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}