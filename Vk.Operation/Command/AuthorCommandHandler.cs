using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class AuthorCommandHandler :
    IRequestHandler<CreateAuthorCommand, ApiResponse<AuthorResponse>>,
    IRequestHandler<DeleteAuthorCommand, ApiResponse>,
    IRequestHandler<DeleteAuthorAllCommand, ApiResponse>,
    IRequestHandler<HardDeleteAuthorCommand, ApiResponse>,
    IRequestHandler<HardDeleteAuthorAllCommand, ApiResponse>,
    IRequestHandler<UpdateAuthorCommand, ApiResponse>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    public AuthorCommandHandler(IMapper mapper,VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    public async Task<ApiResponse<AuthorResponse>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        Author result = dbContext.Set<Author>().SingleOrDefault(x => x.AuthorNumber == request.Model.AuthorNumber);
        
        if (result is not null)
        {
            return new ApiResponse<AuthorResponse>("Error");
        }
        
        Author mapped = mapper.Map<Author>(request.Model);
        mapped.InsertDate = DateTime.UtcNow;
        
        var entity = await dbContext.Set<Author>()
            .AddAsync(mapped, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        AuthorResponse response = mapper.Map<AuthorResponse>(entity.Entity);
        return new ApiResponse<AuthorResponse>(response);
    }
    
    
    public async Task<ApiResponse> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        Author entity = await dbContext.Set<Author>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null)
        {
            return new ApiResponse("Error");
        }
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow; 
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }  

    public async Task<ApiResponse> Handle(DeleteAuthorAllCommand request, CancellationToken cancellationToken)
    {
        List<Author> entities = await dbContext.Set<Author>()
            .ToListAsync(cancellationToken);
        
        entities.ForEach(x =>
        {
            x.InsertDate = DateTime.UtcNow;
            x.IsActive = false;
        });
        dbContext.Set<Author>().UpdateRange(entities);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();

    }

    public async Task<ApiResponse> Handle(HardDeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        Author entity = await dbContext.Set<Author>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null)
        {
            return new ApiResponse("Error");
        }
        dbContext.Set<Author>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(HardDeleteAuthorAllCommand request, CancellationToken cancellationToken)
    {
        List<Author> entities = await dbContext.Set<Author>()
            .ToListAsync();
        if (entities == null)
        {
            return new ApiResponse("Error");
        }
        
        entities.ForEach(x =>
        {
            dbContext.Set<Author>().Remove(x);

        });
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Author>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Error");
        }
        entity.Name = request.Model.Name;
        entity.Surname = request.Model.Surname;
        entity.BirthDate = request.Model.BirthDate;
        entity.UpdateDate = DateTime.UtcNow;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}