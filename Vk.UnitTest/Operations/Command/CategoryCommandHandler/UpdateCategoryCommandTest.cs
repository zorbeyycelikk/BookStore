using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.CategoryCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class UpdateCategoryCommandTest : IClassFixture<CommonTextFixture>
{ 
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
     public UpdateCategoryCommandTest(CommonTextFixture textFixture)
     {
         dbContext = textFixture.dbContext;
         mapper = textFixture.Mapper;
     }
    
     [Theory]
     [InlineData(5555)]
     [InlineData(222)]
     [InlineData(-7)]
    public async  void WhenNonExistentValueIsGivenToUpdateCategory_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        CategoryUpdateRequest test = new CategoryUpdateRequest();
        
        // act(çalıştırma)
        var command = new CategoryCommandHandler(mapper, dbContext);
        var operation = new UpdateCategoryCommand(test , id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}