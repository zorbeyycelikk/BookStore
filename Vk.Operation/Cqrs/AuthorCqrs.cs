using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record GetAllAuthorQuery() : IRequest<ApiResponse<List<AuthorResponse>>>;
public record GetAuthorByIdQuery(int Id) : IRequest<ApiResponse<AuthorResponse>>;
public record DeleteAuthorCommand(int Id) : IRequest<ApiResponse>;
public record DeleteAuthorAllCommand() : IRequest<ApiResponse>;
public record HardDeleteAuthorCommand(int Id) : IRequest<ApiResponse>;
public record HardDeleteAuthorAllCommand() : IRequest<ApiResponse>;
public record UpdateAuthorCommand(AuthorUpdateRequest Model,int Id) : IRequest<ApiResponse>;
public record CreateAuthorCommand(AuthorCreateRequest Model) : IRequest<ApiResponse<AuthorResponse>>;

