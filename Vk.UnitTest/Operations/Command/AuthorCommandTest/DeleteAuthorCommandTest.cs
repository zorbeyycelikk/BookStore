using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;

namespace Vk.UnitTest.Operations.Command.AuthorCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class DeleteAuthorCommandTest : IClassFixture<CommonTextFixture>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
    public DeleteAuthorCommandTest(CommonTextFixture textFixture)
    {
        dbContext = textFixture.dbContext;
        mapper = textFixture.Mapper;
    }
    
    [Theory]
    [InlineData(-10)]
    [InlineData(345)]
    [InlineData(6789)]
    public async  void WhenNonExistentValueIsGivenToDeleteAuthor_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        
        // act(çalıştırma)
        var command = new AuthorCommandHandler(mapper, dbContext);
        var operation = new DeleteAuthorCommand(id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}