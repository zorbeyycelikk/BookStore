using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.BookCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class HardDeleteBookCommandTest : IClassFixture<CommonTextFixture>
{
    // private readonly Mock<VkDbContext> dbContext;
    // private readonly Mock<IMapper> mapper;
    
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
    public HardDeleteBookCommandTest(CommonTextFixture textFixture)
    {
        dbContext = textFixture.dbContext;
        mapper = textFixture.Mapper;
    }
    
    [Theory]
    [InlineData(-10)]
    [InlineData(3459)]
    [InlineData(672)]
    public async  void WhenNonExistentValueIsGivenToHardDeleteBook_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        
        // act(çalıştırma)
        var command = new BookCommandHandler(mapper, dbContext);
        var operation = new HardDeleteBookCommand(id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}