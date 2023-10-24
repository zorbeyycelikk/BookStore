using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.UserCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class UpdateUserCommandTest : IClassFixture<CommonTextFixture>
{ 
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
     public UpdateUserCommandTest(CommonTextFixture textFixture)
     {
         dbContext = textFixture.dbContext;
         mapper = textFixture.Mapper;
     }
    
     [Theory]
     [InlineData(5555)]
     [InlineData(222)]
     [InlineData(-7)]
    public async  void WhenNonExistentValueIsGivenToUpdateUser_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        UserUpdateRequest test = new UserUpdateRequest();
        
        // act(çalıştırma)
        var command = new UserCommandHandler(mapper, dbContext);
        var operation = new UpdateUserCommand(test , id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}