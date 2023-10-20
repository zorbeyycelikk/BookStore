using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vk.Base.Response;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]

public class SessionUserController : ControllerBase
{
    private readonly IMediator mediator;
    public SessionUserController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ApiResponse<UserResponse>> GetSessionInfo()
    {
            var id = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetSessionUserByIdQuery(int.Parse(id));
            var result = await mediator.Send(operation);
            return result;
    }
}