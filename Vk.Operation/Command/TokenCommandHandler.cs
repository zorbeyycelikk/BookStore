using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vk.Base;
using Vk.Base.Response;
using Vk.Base.Token;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Cqrs;

namespace Vk.Operation.Command;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
    private readonly VkDbContext dbContext;
    private readonly JwtConfig jwtConfig;

    
    public TokenCommandHandler(VkDbContext dbContext,IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig.CurrentValue;
    }


    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.UserNumber == request.Model.UserNumber, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<TokenResponse>("User not found");
        }
        
        var md5 = Md5.Create(request.Model.Password.ToUpper());
        if (entity.Password != md5)
        {
            entity.LastActivityDate = DateTime.UtcNow;
            entity.PasswordRetryCount++;
            await dbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse<TokenResponse>("Password Not Valid");
        }
        
        if (!entity.IsActive)
        {
            return new ApiResponse<TokenResponse>("User is not active");
        }
        
        string token = Token(entity);
        TokenResponse tokenResponse = new()
        {
            Token = token,
            ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            UserNumber = entity.UserNumber,
            Email = entity.Email
        };
        return new ApiResponse<TokenResponse>(tokenResponse);
    }
    
    private string Token(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }
    
    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("CustomerNumber", user.UserNumber.ToString()),
            new Claim("Role", user.Role),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("FullName", $"{user.FirstName}, {user.LastName}")
        };

        return claims;
    }
}