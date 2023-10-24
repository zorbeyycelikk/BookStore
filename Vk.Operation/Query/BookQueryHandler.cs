using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class BookQueryHandler :
    IRequestHandler<GetAllBookQuery, ApiResponse<List<BookResponse>>>,
    IRequestHandler<GetBookByIdQuery, ApiResponse<BookResponse>>
    
{
   // private readonly VkDbContext dbContext;
   private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public BookQueryHandler(IMapper mapper,VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<BookResponse>>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
    {
        List<Book> entities = await dbContext.Set<Book>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .ToListAsync(cancellationToken);
       
        List<BookResponse> mapped = mapper.Map<List<BookResponse>>(entities);
        return new ApiResponse<List<BookResponse>>(mapped);
    }

    public async Task<ApiResponse<BookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        Book entity = await dbContext.Set<Book>().Include(x => x.Author)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (entity == null)
        {
            throw new InvalidOperationException("Error");
        }

        BookResponse mapped = mapper.Map<BookResponse>(entity);
        return new ApiResponse<BookResponse>(mapped);
    }
}