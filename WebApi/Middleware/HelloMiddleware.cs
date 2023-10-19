using Azure.Core;

namespace WebApi.Middleware;

public class HelloMiddleware
{
    private readonly RequestDelegate next;
    
    // next bir sonraki middleware'ı tutar
    public HelloMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine("Hello World !");
        await next.Invoke(context);
        Console.WriteLine("Bye World ! ");
    }
    
}

static public class HelloMiddlewareExtension
{
    public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HelloMiddleware>();
    }
}