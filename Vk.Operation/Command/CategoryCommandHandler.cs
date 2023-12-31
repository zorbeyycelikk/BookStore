using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class CategoryCommandHandler :
    IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryResponse>>,
    IRequestHandler<DeleteCategoryCommand, ApiResponse>,
    IRequestHandler<DeleteCategoryAllCommand, ApiResponse>,
    IRequestHandler<HardDeleteCategoryCommand, ApiResponse>,
    IRequestHandler<HardDeleteCategoryAllCommand, ApiResponse>,
    IRequestHandler<UpdateCategoryCommand, ApiResponse>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    public CategoryCommandHandler(IMapper mapper,VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    public async Task<ApiResponse<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        
        Category result = dbContext.Set<Category>().SingleOrDefault(x => x.CategoryNumber == request.Model.CategoryNumber);
        
        if (result is not null)
        {
            return new ApiResponse<CategoryResponse>("Error");
        }
        
        Category mapped = mapper.Map<Category>(request.Model);
        mapped.InsertDate = DateTime.UtcNow;
        
        var entity = await dbContext.Set<Category>()
            .AddAsync(mapped, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        CategoryResponse response = mapper.Map<CategoryResponse>(entity.Entity);
        return new ApiResponse<CategoryResponse>(response);
    }
    
    
    public async Task<ApiResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category entity = await dbContext.Set<Category>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null)
        {
            return new ApiResponse("Error");
        }
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow; 
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }

    public async Task<ApiResponse> Handle(DeleteCategoryAllCommand request, CancellationToken cancellationToken)
    {
        List<Category> entities = await dbContext.Set<Category>()
            .ToListAsync(cancellationToken);
        
        entities.ForEach(x =>
        {
            x.InsertDate = DateTime.UtcNow;
            x.IsActive = false;
        });
        dbContext.Set<Category>().UpdateRange(entities);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }

    public async Task<ApiResponse> Handle(HardDeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category entity = await dbContext.Set<Category>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null)
        {
            return new ApiResponse("Error");
        }
        dbContext.Set<Category>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(HardDeleteCategoryAllCommand request, CancellationToken cancellationToken)
    {
        List<Category> entities = await dbContext.Set<Category>()
            .ToListAsync();
        if (entities == null)
        {
            return new ApiResponse("Error");
        }
        
        entities.ForEach(x =>
        {
            dbContext.Set<Category>().Remove(x);

        });
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Category>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Error");
        }
        entity.CategoryName = request.Model.CategoryName;
        entity.UpdateDate = DateTime.UtcNow;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}