using MediatR;
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
    public  async Task<ApiResponse<List<CategoryResponse>>> GetAll()
    { 
        try
        {
            this.HttpContext.Response.StatusCode = 200;
            var operation = new GetAllCategoryQuery();
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
    public async Task<ApiResponse<CategoryResponse>> GetById(int id)
    {
            var operation = new GetCategoryByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
    }
    
    [HttpPost]
    public async Task<ApiResponse<CategoryResponse>> Create([FromBody] CategoryCreateRequest request)
    {
        var operation = new CreateCategoryCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete]
    public async Task<ApiResponse> DeleteAll()
    {
        var operation = new DeleteCategoryAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("{id}")]
    public async Task<ApiResponse> DeleteById(int id)
    {
        var operation = new DeleteCategoryCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete")]
    public async Task<ApiResponse> HardDeleteAll()
    {
        var operation = new HardDeleteCategoryAllCommand();
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpDelete("HardDelete {id}")]
    public async Task<ApiResponse> HardDeleteById(int id)
    {
        var operation = new HardDeleteCategoryCommand(id);
        var result = await mediator.Send(operation);
        return result;
    }
    
    [HttpPut("{id}")]
    public async Task<ApiResponse> Update(int id,[FromBody] CategoryUpdateRequest request)
    {
        var operation = new UpdateCategoryCommand(request, id);
        var result = await mediator.Send(operation);
        return result;
    }

}