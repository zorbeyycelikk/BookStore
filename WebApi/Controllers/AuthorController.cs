using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class AuthorController : ControllerBase
{
    private readonly IMediator mediator;
    public AuthorController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public  async Task <IActionResult> GetAll()
    { 
            var operation = new GetAllAuthorQuery();
            var result = await mediator.Send(operation);
            return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task <IActionResult> GetById(int id)
    {
            var operation = new GetAuthorByIdQuery(id);
            var result = await mediator.Send(operation);
            return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> Create([FromBody] AuthorCreateRequest request)
    {
        var operation = new CreateAuthorCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> DeleteAll()
    {
        var operation = new DeleteAuthorAllCommand();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> DeleteById(int id)
    {
        var operation = new DeleteAuthorCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("HardDelete")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> HardDeleteAll()
    {
        var operation = new HardDeleteAuthorAllCommand();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("HardDelete {id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> HardDeleteById(int id)
    {
        var operation = new HardDeleteAuthorCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> Update(int id,[FromBody] AuthorUpdateRequest request)
    {
        var operation = new UpdateAuthorCommand(request, id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}