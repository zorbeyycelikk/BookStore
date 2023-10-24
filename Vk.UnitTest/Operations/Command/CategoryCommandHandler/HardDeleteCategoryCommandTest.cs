using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;

namespace Vk.UnitTest.Operations.Command.CategoryCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class HardDeleteCategoryCommandTest : IClassFixture<CommonTextFixture>
{ 
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
    public HardDeleteCategoryCommandTest(CommonTextFixture textFixture)
    {
        dbContext = textFixture.dbContext;
        mapper = textFixture.Mapper;
    }
    
    [Theory]
    [InlineData(-10)]
    [InlineData(3459)]
    [InlineData(672)]
    public async  void WhenNonExistentValueIsGivenToHardDeleteCategory_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        
        // act(çalıştırma)
        var command = new CategoryCommandHandler(mapper, dbContext);
        var operation = new HardDeleteCategoryCommand(id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}