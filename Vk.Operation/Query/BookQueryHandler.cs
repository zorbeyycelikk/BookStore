using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Uow;
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
    private readonly IUnitOfWork unitOfWork;

    public BookQueryHandler(IMapper mapper, IUnitOfWork unitOfWork,VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }
    
    public async Task<ApiResponse<List<BookResponse>>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
    {
        // List<Book> entities = await unitOfWork.BookRepository.GetAll(cancellationToken, "Author", "Category");
       
        List<Book> entities = await dbContext.Set<Book>()
            .Include(x => x.Author)
            .Include(x => x.Category)
            .ToListAsync(cancellationToken);
       
        List<BookResponse> mapped = mapper.Map<List<BookResponse>>(entities);
        return new ApiResponse<List<BookResponse>>(mapped);
    }

    public async Task<ApiResponse<BookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        // var entity = await unitOfWork.BookRepository.GetById(request.Id,cancellationToken,"Author","Category");
        
        Book entity = await dbContext.Set<Book>().Include(x => x.Author)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (entity == null)
        {
            return new ApiResponse<BookResponse>("Record not found!");
        }

        BookResponse mapped = mapper.Map<BookResponse>(entity);
        return new ApiResponse<BookResponse>(mapped);
    }
}