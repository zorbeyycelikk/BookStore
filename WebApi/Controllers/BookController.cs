using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,User")]
    public  async Task <IActionResult> GetAll()
    { 
            var operation = new GetAllBookQuery();
            var result = await mediator.Send(operation);
            return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task <IActionResult>  GetById(int id)
    {
            var operation = new GetBookByIdQuery(id);
            var result = await mediator.Send(operation);
            return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> Create([FromBody] BookCreateRequest request)
    {
        var operation = new CreateBookCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> DeleteAll()
    {
        var operation = new DeleteBookAllCommand();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> DeleteById(int id)
    {
        var operation = new DeleteBookCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("HardDelete")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> HardDeleteAll()
    {
        var operation = new HardDeleteBookAllCommand();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("HardDelete {id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> HardDeleteById(int id)
    {
        var operation = new HardDeleteBookCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> Update(int id,[FromBody] BookUpdateRequest request)
    {
        var operation = new UpdateBookCommand(request, id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }

}