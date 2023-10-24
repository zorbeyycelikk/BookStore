using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.AuthorCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class UpdateAuthorCommandTest : IClassFixture<CommonTextFixture>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
     public UpdateAuthorCommandTest(CommonTextFixture textFixture)
     {
         dbContext = textFixture.dbContext;
         mapper = textFixture.Mapper;
     }
    
    [Theory]
    [InlineData(9999)]
    [InlineData(1000)]
    [InlineData(-5)]
    public async  void WhenNonExistentValueIsGivenToUpdateAuthor_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        AuthorUpdateRequest test = new AuthorUpdateRequest();
        
        // act(çalıştırma)
        var command = new AuthorCommandHandler(mapper, dbContext);
        var operation = new UpdateAuthorCommand(test , id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}