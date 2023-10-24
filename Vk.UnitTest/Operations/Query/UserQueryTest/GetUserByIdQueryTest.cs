using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Operation.Query;

namespace Vk.UnitTest.Operations.Command.UserCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class GetUserByIdQueryTest : IClassFixture<CommonTextFixture>
{
    private readonly VkDbContext dbContext;
    private readonly IMapper mapper;
    
    public GetUserByIdQueryTest(CommonTextFixture textFixture)
    {
        dbContext = textFixture.dbContext;
        mapper = textFixture.Mapper;
    }
    
    [Theory]
    [InlineData(-10)]
    [InlineData(345)]
    [InlineData(6789)]
    public async  void WhenNonExistentValueIsGivenToGetUserById_ResponseMessageError_ShouldBeReturn(int id)
    {
        // arrange( hazırlık)
        
        // act(çalıştırma)
        var command = new UserQueryHandler(mapper, dbContext);
        var operation = new GetUserByIdQuery(id);
        
        // assert(dogrulama)
        var result = await command.Handle(operation, default);
        result.Message.Should().Be("Error");
    }
}