using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Base;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.Operation.Command;

public class UserCommandHandler :
    IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>,
    IRequestHandler<DeleteUserCommand, ApiResponse>,
    IRequestHandler<DeleteUserAllCommand, ApiResponse>,
    IRequestHandler<HardDeleteUserCommand, ApiResponse>,
    IRequestHandler<HardDeleteUserAllCommand, ApiResponse>,
    IRequestHandler<UpdateUserCommand, ApiResponse>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    public UserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork,VkDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }
    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User mapped = mapper.Map<User>(request.Model);
        
        var md5 = Md5.Create(request.Model.Password.ToUpper());
        mapped.Password = md5;
        mapped.InsertDate = DateTime.UtcNow;

        var entity = await dbContext.Set<User>()
            .AddAsync(mapped, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        UserResponse response = mapper.Map<UserResponse>(entity.Entity);
        return new ApiResponse<UserResponse>(response);
    }
    
    
    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow; 
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Record Found And Delete Succes");

    }

    public async Task<ApiResponse> Handle(DeleteUserAllCommand request, CancellationToken cancellationToken)
    {
        List<User> entities = await dbContext.Set<User>()
            .ToListAsync(cancellationToken);
        
        entities.ForEach(x =>
        {
            x.UpdateDate = DateTime.UtcNow;
            x.IsActive = false;
        });
        dbContext.Set<User>().UpdateRange(entities);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Record Found And Delete Succes");

    }

    public async Task<ApiResponse> Handle(HardDeleteUserCommand request, CancellationToken cancellationToken)
    {
        User entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        dbContext.Set<User>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Record Found And Delete Succes");
    }

    public async Task<ApiResponse> Handle(HardDeleteUserAllCommand request, CancellationToken cancellationToken)
    {
        List<User> entities = await dbContext.Set<User>()
            .ToListAsync();
        if (entities == null)
        {
            return new ApiResponse("Records not found!");
        }
        
        entities.ForEach(x =>
        {
            dbContext.Set<User>().Remove(x);

        });
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse("Record Found And Delete Succes");
    }

    public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }
        entity.FirstName = request.Model.FirstName;
        entity.LastName = request.Model.LastName;
        entity.Email = request.Model.Email;
        entity.Password = Md5.Create(request.Model.Password.ToUpper());
        entity.UpdateDate = DateTime.UtcNow;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}