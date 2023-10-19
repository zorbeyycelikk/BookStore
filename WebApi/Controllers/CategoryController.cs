using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class CategoryController : ControllerBase
{
    private readonly IMediator mediator;
    public CategoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public  async Task<ApiResponse<List<CategoryResponse>>> GetAll()
    { 
            var operation = new GetAllCategoryQuery();
            var result = await mediator.Send(operation);
            return result;
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<ApiResponse<CategoryResponse>> GetById(int id)
    {
            var operation = new GetCategoryByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<CategoryResponse>> Create([FromBody] CategoryCreateRequest request)
    {
        var operation = new CreateCategoryCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> DeleteAll()
    {
        var operation = new DeleteCategoryAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> DeleteById(int id)
    {
        var operation = new DeleteCategoryCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> HardDeleteAll()
    {
        var operation = new HardDeleteCategoryAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete {id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> HardDeleteById(int id)
    {
        var operation = new HardDeleteCategoryCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Update(int id,[FromBody] CategoryUpdateRequest request)
    {
        var operation = new UpdateCategoryCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }
}