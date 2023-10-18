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
        try
        {
            this.HttpContext.Response.StatusCode = 200;
            var operation = new GetAllBookQuery();
            var result = await mediator.Send(operation);
            return result;
        }
        catch (Exception ex)
        {
            // Hata durumunda bir mesaj fırlat
            this.HttpContext.Response.StatusCode = 500;
            throw new Exception("Veri çekme işlemi sırasında bir hata oluştu.", ex);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ApiResponse<BookResponse>> Get(int id)
    {
        try
        {
            this.HttpContext.Response.StatusCode = 200;
            var operation = new GetBookByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }
        catch (Exception ex)
        {
            // Hata durumunda bir mesaj fırlat

            this.HttpContext.Response.StatusCode = 500;
            throw new Exception("Veri çekme işlemi sırasında bir hata oluştu.", ex);
        }
    }
    
    [HttpPost]
    public async Task<ApiResponse> Post([FromBody] BookCreateRequest request)
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

}