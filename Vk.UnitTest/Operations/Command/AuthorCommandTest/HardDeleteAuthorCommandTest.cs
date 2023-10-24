using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.AuthorCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class HardDeleteAuthorCommandTest : IClassFixture<CommonTextFixture>
{
    // private readonly Mock<VkDbContext> dbContext;
    // private readonly Mock<IMapper> mapper;
    
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
    public HardDeleteAuthorCommandTest(CommonTextFixture textFixture)
    {
        dbContext = textFixture.dbContext;
        mapper = textFixture.Mapper;
    }
    
    [Theory]
    [InlineData(-11)]
    [InlineData(234)]
    [InlineData(5678)]
    public async  void WhenNonExistentValueIsGivenToHardDeleteAuthor_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        
        // act(çalıştırma)
        var command = new AuthorCommandHandler(mapper, dbContext);
        var operation = new HardDeleteAuthorCommand(id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}