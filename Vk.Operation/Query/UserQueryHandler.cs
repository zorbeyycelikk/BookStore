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

public class UserQueryHandler :
    IRequestHandler<GetAllUserQuery, ApiResponse<List<UserResponse>>>,
    IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
    
{
   // private readonly VkDbContext dbContext;
   private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;

    public UserQueryHandler(IMapper mapper, IUnitOfWork unitOfWork,VkDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }
    
    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        // List<User> entities = await unitOfWork.UserRepository.GetAll(cancellationToken, "Author", "User");
       
        List<User> entities = await dbContext.Set<User>()
            .ToListAsync(cancellationToken);
       
        List<UserResponse> mapped = mapper.Map<List<UserResponse>>(entities);
        return new ApiResponse<List<UserResponse>>(mapped);
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // var entity = await unitOfWork.UserRepository.GetById(request.Id,cancellationToken,"Author","User");
        
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