using MediatR;
using Vk.Base.Response;
using Vk.Schema;

namespace Vk.Operation.Cqrs;

public record GetAllBookQuery() : IRequest<ApiResponse<List<BookResponse>>>;
public record GetBookByIdQuery(int Id) : IRequest<ApiResponse<BookResponse>>;
public record DeleteBookCommand(int Id) : IRequest<ApiResponse>;
public record DeleteBookAllCommand() : IRequest<ApiResponse>;
public record HardDeleteBookCommand(int Id) : IRequest<ApiResponse>;
public record HardDeleteBookAllCommand() : IRequest<ApiResponse>;
public record UpdateBookCommand(BookUpdateRequest Model,int Id) : IRequest<ApiResponse>;
public record CreateBookCommand(BookCreateRequest Model) : IRequest<ApiResponse<BookResponse>>;

