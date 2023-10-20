using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record GetSessionUserByIdQuery(int Id) : IRequest<ApiResponse<UserResponse>>;
