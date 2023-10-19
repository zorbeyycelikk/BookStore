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

public class CategoryQueryHandler :
    IRequestHandler<GetAllCategoryQuery, ApiResponse<List<CategoryResponse>>>,
    IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryResponse>>
    
{
   // private readonly VkDbContext dbContext;
   private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public CategoryQueryHandler(IMapper mapper, IUnitOfWork unitOfWork,VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }
    
    public async Task<ApiResponse<List<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        // List<Category> entities = await unitOfWork.CategoryRepository.GetAll(cancellationToken, "Author", "Category");
       
        List<Category> entities = await dbContext.Set<Category>()
            .ToListAsync(cancellationToken);
       
        List<CategoryResponse> mapped = mapper.Map<List<CategoryResponse>>(entities);
        return new ApiResponse<List<CategoryResponse>>(mapped);
    }

    public async Task<ApiResponse<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        // var entity = await unitOfWork.CategoryRepository.GetById(request.Id,cancellationToken,"Author","Category");
        
        Category entity = await dbContext.Set<Category>()
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (entity == null)
        {
            return new ApiResponse<CategoryResponse>("Record not found!");
        }
        CategoryResponse mapped = mapper.Map<CategoryResponse>(entity);
        return new ApiResponse<CategoryResponse>(mapped);
    }
}