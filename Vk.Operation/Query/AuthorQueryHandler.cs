using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Query;

public class AuthorQueryHandler :
    IRequestHandler<GetAllAuthorQuery, ApiResponse<List<AuthorResponse>>>,
    IRequestHandler<GetAuthorByIdQuery, ApiResponse<AuthorResponse>>
    
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;

    public AuthorQueryHandler(IMapper mapper,VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    
    public async Task<ApiResponse<List<AuthorResponse>>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
    {
        List<Author> entities = await dbContext.Set<Author>()
            .ToListAsync(cancellationToken);
       
        List<AuthorResponse> mapped = mapper.Map<List<AuthorResponse>>(entities);
        return new ApiResponse<List<AuthorResponse>>(mapped);
    }

    public async Task<ApiResponse<AuthorResponse>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        Author entity = await dbContext.Set<Author>()
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (entity == null)
        {
            return new ApiResponse<AuthorResponse>("Error");
        }

        AuthorResponse mapped = mapper.Map<AuthorResponse>(entity);
        return new ApiResponse<AuthorResponse>(mapped);
    }
}