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

public class SessionUserQueryHandler :
    IRequestHandler<GetSessionUserByIdQuery, ApiResponse<UserResponse>>
{
    
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public SessionUserQueryHandler(IMapper mapper,VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public async Task<ApiResponse<UserResponse>> Handle(GetSessionUserByIdQuery request, CancellationToken cancellationToken)
    {
        User entity = await dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (entity == null)
        {
            return new ApiResponse<UserResponse>("Record not found!");
        }
        UserResponse mapped = mapper.Map<UserResponse>(entity);
        return new ApiResponse<UserResponse>(mapped);
    }
}