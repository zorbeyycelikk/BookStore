using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vk.Data.Context;
using Vk.Operation.Mapper;
using Vk.UnitTest.TestSetup;

namespace Vk.UnitTest.Operations;

public class CommonTextFixture
{
    public VkDbContext dbContext { get; set; }
    
    public IMapper Mapper { get; set; }
    
    public IMediator mediator{ get; set; }


    public CommonTextFixture()
    {
        var options = new DbContextOptionsBuilder<VkDbContext>().UseInMemoryDatabase(databaseName: "BookTestDB").Options;
        dbContext = new VkDbContext(options);
        dbContext.Database.EnsureCreated(); // database'in yaratıldığından emin oluyor
        
        // Veri tabanını taklit ettik ve örnek datalarla beraber oluşturup dbyi ayağa kaldırdık.
        dbContext.AddBooks();
        dbContext.AddCategory();
        dbContext.AddAuthor();
        dbContext.SaveChanges();

        // Mapper Eklendi
        Mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperConfig>();
        }).CreateMapper();
        
        // MediatR
    }

}