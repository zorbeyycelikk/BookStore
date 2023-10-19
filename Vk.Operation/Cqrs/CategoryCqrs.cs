using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record GetAllCategoryQuery() : IRequest<ApiResponse<List<CategoryResponse>>>;
public record GetCategoryByIdQuery(int Id) : IRequest<ApiResponse<CategoryResponse>>;
public record DeleteCategoryCommand(int Id) : IRequest<ApiResponse>;
public record DeleteCategoryAllCommand() : IRequest<ApiResponse>;
public record HardDeleteCategoryCommand(int Id) : IRequest<ApiResponse>;
public record HardDeleteCategoryAllCommand() : IRequest<ApiResponse>;
public record UpdateCategoryCommand(CategoryUpdateRequest Model,int Id) : IRequest<ApiResponse>;
public record CreateCategoryCommand(CategoryCreateRequest Model) : IRequest<ApiResponse<CategoryResponse>>;

