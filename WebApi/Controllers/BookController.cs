using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class BookController : ControllerBase
{
    private readonly IMediator mediator;
    public BookController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    public  async Task<ApiResponse<List<BookResponse>>> GetAll()
    { 
            var operation = new GetAllBookQuery();
            var result = await mediator.Send(operation);
            return result;
    }
    
    [HttpGet("{id}")]
    public async Task<ApiResponse<BookResponse>> GetById(int id)
    {
            var operation = new GetBookByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
    }
    
    [HttpPost]
    public async Task<ApiResponse<BookResponse>> Create([FromBody] BookCreateRequest request)
    {
        var operation = new CreateBookCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete]
    public async Task<ApiResponse> DeleteAll()
    {
        var operation = new DeleteBookAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("{id}")]
    public async Task<ApiResponse> DeleteById(int id)
    {
        var operation = new DeleteBookCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete")]
    public async Task<ApiResponse> HardDeleteAll()
    {
        var operation = new HardDeleteBookAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete {id}")]
    public async Task<ApiResponse> HardDeleteById(int id)
    {
        var operation = new HardDeleteBookCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpPut("{id}")]
    public async Task<ApiResponse> Update(int id,[FromBody] BookUpdateRequest request)
    {
        var operation = new UpdateBookCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

}