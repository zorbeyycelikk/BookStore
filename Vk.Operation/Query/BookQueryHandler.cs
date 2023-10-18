using AutoMapper;
using MediatR;
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
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public BookQueryHandler(VkDbContext dbContext, IMapper mapper, IUnitOfWork unitOfWork)
    {
        // this.dbContext = dbContext;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }
    
    public async Task<ApiResponse<List<BookResponse>>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
    {
        List<Book> entity = unitOfWork.BookRepository.GetAll();
        List<BookResponse> mapped = mapper.Map<List<BookResponse>>(entity);
        return new ApiResponse<List<BookResponse>>(mapped);
    }

    public async Task<ApiResponse<BookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = unitOfWork.BookRepository.GetById(request.Id);
        BookResponse mapped = mapper.Map<BookResponse>(entity);
        return new ApiResponse<BookResponse>(mapped);
    }
}