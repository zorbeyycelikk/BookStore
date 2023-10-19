using MediatR;
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
    public  async Task<ApiResponse<List<AuthorResponse>>> GetAll()
    { 
        try
        {
            this.HttpContext.Response.StatusCode = 200;
            var operation = new GetAllAuthorQuery();
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
    public async Task<ApiResponse<AuthorResponse>> GetById(int id)
    {
        try
        {
            this.HttpContext.Response.StatusCode = 200;
            var operation = new GetAuthorByIdQuery(id);
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
    public async Task<ApiResponse<AuthorResponse>> Create([FromBody] AuthorCreateRequest request)
    {
        var operation = new CreateAuthorCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete]
    public async Task<ApiResponse> DeleteAll()
    {
        var operation = new DeleteAuthorAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("{id}")]
    public async Task<ApiResponse> DeleteById(int id)
    {
        var operation = new DeleteAuthorCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete")]
    public async Task<ApiResponse> HardDeleteAll()
    {
        var operation = new HardDeleteAuthorAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete {id}")]
    public async Task<ApiResponse> HardDeleteById(int id)
    {
        var operation = new HardDeleteAuthorCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpPut("{id}")]
    public async Task<ApiResponse> Update(int id,[FromBody] AuthorUpdateRequest request)
    {
        var operation = new UpdateAuthorCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

}