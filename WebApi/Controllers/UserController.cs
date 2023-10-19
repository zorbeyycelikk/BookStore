using MediatR;
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
    public  async Task<ApiResponse<List<UserResponse>>> GetAll()
    { 
        try
        {
            this.HttpContext.Response.StatusCode = 200;
            var operation = new GetAllUserQuery();
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
    public async Task<ApiResponse<UserResponse>> GetById(int id)
    {
            var operation = new GetUserByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
    }
    
    [HttpPost]
    public async Task<ApiResponse<UserResponse>> Create([FromBody] UserCreateRequest request)
    {
        var operation = new CreateUserCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete]
    public async Task<ApiResponse> DeleteAll()
    {
        var operation = new DeleteUserAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("{id}")]
    public async Task<ApiResponse> DeleteById(int id)
    {
        var operation = new DeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete")]
    public async Task<ApiResponse> HardDeleteAll()
    {
        var operation = new HardDeleteUserAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete {id}")]
    public async Task<ApiResponse> HardDeleteById(int id)
    {
        var operation = new HardDeleteUserCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpPut("{id}")]
    public async Task<ApiResponse> Update(int id,[FromBody] UserUpdateRequest request)
    {
        var operation = new UpdateUserCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

}