using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class UserController : ControllerBase
{
    private readonly IMediator mediator;
    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public  async Task <IActionResult> GetAll()
    { 
            var operation = new GetAllUserQuery();
            var result = await mediator.Send(operation);
            return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> GetById(int id)
    {
            var operation = new GetUserByIdQuery(id);
            var result = await mediator.Send(operation);
            return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> Create2([FromBody] UserCreateRequest request)
    {
        var operation = new CreateUserCommand(request);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Response) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> DeleteAll()
    {
        var operation = new DeleteUserAllCommand();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult>DeleteById(int id)
    {
        var operation = new DeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("HardDelete")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> HardDeleteAll()
    {
        var operation = new HardDeleteUserAllCommand();
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpDelete("HardDelete {id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> HardDeleteById(int id)
    {
        var operation = new HardDeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task <IActionResult> Update(int id,[FromBody] UserUpdateRequest request)
    {
        var operation = new UpdateUserCommand(request, id);
        var result = await mediator.Send(operation);
        return result.Success ? Ok(result.Message) : result.Message == "Error" ? NotFound() : BadRequest();
    }
}