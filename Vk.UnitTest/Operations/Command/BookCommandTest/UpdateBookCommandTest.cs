using AutoMapper;
using FluentAssertions;
using Moq;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.BookCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class UpdateBookCommandTest : IClassFixture<CommonTextFixture>
{
    // private readonly Mock<VkDbContext> dbContext;
    // private readonly Mock<IMapper> mapper;
    
     private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
     public UpdateBookCommandTest(CommonTextFixture textFixture)
     {
         dbContext = textFixture.dbContext;
         mapper = textFixture.Mapper;
     }
    
     [Theory]
     [InlineData(5555)]
     [InlineData(222)]
     [InlineData(-7)]
    public async  void WhenNonExistentValueIsGivenToUpdateBook_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        BookUpdateRequest test = new BookUpdateRequest();
        
        // act(çalıştırma)
        var command = new BookCommandHandler(mapper, dbContext);
        var operation = new UpdateBookCommand(test , id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}