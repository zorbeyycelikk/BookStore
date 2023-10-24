using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;

namespace Vk.UnitTest.Operations.Command.BookCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class DeleteBookCommandTest : IClassFixture<CommonTextFixture>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
    public DeleteBookCommandTest(CommonTextFixture textFixture)
    {
        dbContext = textFixture.dbContext;
        mapper = textFixture.Mapper;
    }
    
    [Theory]
    [InlineData(-12)]
    [InlineData(34567)]
    [InlineData(678)]
    public async  void WhenNonExistentValueIsGivenToDeleteBook_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        
        // act(çalıştırma)
        var command = new BookCommandHandler(mapper, dbContext);
        var operation = new DeleteBookCommand(id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}