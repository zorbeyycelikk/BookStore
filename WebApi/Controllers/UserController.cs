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
    public  async Task<ApiResponse<List<UserResponse>>> GetAll()
    { 
            var operation = new GetAllUserQuery();
            var result = await mediator.Send(operation);
            return result;
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<UserResponse>> GetById(int id)
    {
            var operation = new GetUserByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<UserResponse>> Create([FromBody] UserCreateRequest request)
    {
        var operation = new CreateUserCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> DeleteAll()
    {
        var operation = new DeleteUserAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> DeleteById(int id)
    {
        var operation = new DeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> HardDeleteAll()
    {
        var operation = new HardDeleteUserAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete {id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> HardDeleteById(int id)
    {
        var operation = new HardDeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Update(int id,[FromBody] UserUpdateRequest request)
    {
        var operation = new UpdateUserCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }
}