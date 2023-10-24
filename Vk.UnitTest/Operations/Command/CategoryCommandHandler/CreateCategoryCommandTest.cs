using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.CategoryCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class CreateCategoryCommandTest : IClassFixture<CommonTextFixture>
{
     private readonly VkDbContext dbContext;
     private readonly IMapper mapper;
    
     public CreateCategoryCommandTest(CommonTextFixture textFixture)
     {
         dbContext = textFixture.dbContext;
         mapper = textFixture.Mapper;
     }
    
    [Fact]
    public async void WhenAlreadyExistCategoryNumberIsGivenToCreateCategory_ResponseMessageError_ShouldBeReturn()
    {
        // arrange( hazırlık)
            // Test aşamasında bir data yaratsın ve bitince silmesi için bunu yarattık
        
        var Category = new Category()
        {
            CategoryName = "Test Category" ,
            CategoryNumber = 100
            
        };
        dbContext.Add(Category);
        dbContext.SaveChanges();
        
        // act(çalıştırma)
        var handler = new CategoryCommandHandler(mapper, dbContext);
        CategoryCreateRequest mapped = mapper.Map<CategoryCreateRequest>(Category);
        var operation = new CreateCategoryCommand(mapped);
        var result = await handler.Handle(operation, default);
        
        // assert(dogrulama)
        result.Message.Should().Be("Error");
    }
}