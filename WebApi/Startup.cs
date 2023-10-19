using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Vk.Data.Context;
using Vk.Data.Uow;
using Vk.Operation.Cqrs;
using Vk.Operation.Mapper;
using Vk.Operation.Validation;
using WebApi.Middleware;

namespace WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Database için bağlantı kodları
        string connection = Configuration.GetConnectionString("MsSqlConnection");
        services.AddDbContext<VkDbContext>(opts => opts.UseSqlServer(connection));
        
        // UnitOfWork Config
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        //MediatR
        services.AddMediatR(typeof(CreateBookCommand).GetTypeInfo().Assembly);
        
        // Mapper Configuration
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
        services.AddSingleton(config.CreateMapper());
        
        // FluentValidation
        services.AddControllers().AddFluentValidation(x =>
        {
            x.RegisterValidatorsFromAssemblyContaining<BaseValidator>();
        });
        
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vk.Api", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vk.Api v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseHello();
        // app.Run(); Middleware1 yazar fakat 2 çalışmaz.Run methodu kısa devre yaptırmaya yarar
        // app.Run(async context => Console.WriteLine("Middleware1"));
        // app.Run(async context => Console.WriteLine("Middleware2"));
        
        // app.Use(); next.ınvoke ile bir sonraki middleware'in çalışmasını sağlıyor.
        // app.Use(async(context, next) =>
        // {
        //     Console.WriteLine("Middleware1 Basladi");
        //     await next.Invoke();
        //     Console.WriteLine("Middleware1 Bitti");
        // });
        // app.Use(async(context, next) =>
        // {
        //     Console.WriteLine("Middleware2 Basladi");
        //     await next.Invoke();
        //     Console.WriteLine("Middleware2 Bitti");
        // });
        // app.Use(async(context, next) =>
        // {
        //     Console.WriteLine("Middleware3 Basladi");
        //     await next.Invoke();
        //     Console.WriteLine("Middleware3 Bitti");
        // });
        //
        // // /example ' a gidilince bu çalışır onun dışında çalışmaz
        // app.Map("/example", internalApp => internalApp.Run(async context =>
        // {
        //     Console.WriteLine("Example middleware tetiklendi");
        //     await context.Response.WriteAsync("Example middleware tetiklendi");
        // }));
        //
        // // app.MapWhen() path ile ulaşrıız "GET" && X.REQUEST HTTP  FELAN YÖNETİLEBİLİR
        // app.MapWhen(x => x.Request.Method == "GET" , 
        //     internalApp =>
        // {
        //     internalApp.Run(async context =>
        //     {
        //         Console.WriteLine("MapWhen MiddleWare Tetiklendi");
        //         await context.Response.WriteAsync("MapWhen MiddleWare Tetiklendi");
        //     });
        // });
        //
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}