using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record GetAllUserQuery() : IRequest<ApiResponse<List<UserResponse>>>;
public record GetUserByIdQuery(int Id) : IRequest<ApiResponse<UserResponse>>;
public record DeleteUserCommand(int Id) : IRequest<ApiResponse>;
public record DeleteUserAllCommand() : IRequest<ApiResponse>;
public record HardDeleteUserCommand(int Id) : IRequest<ApiResponse>;
public record HardDeleteUserAllCommand() : IRequest<ApiResponse>;
public record UpdateUserCommand(UserUpdateRequest Model,int Id) : IRequest<ApiResponse>;
public record CreateUserCommand(UserCreateRequest Model) : IRequest<ApiResponse<UserResponse>>;