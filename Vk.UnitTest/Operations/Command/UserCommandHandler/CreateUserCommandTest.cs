using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.UserCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class CreateUserCommandTest : IClassFixture<CommonTextFixture>
{
     private readonly VkDbContext dbContext;
     private readonly IMapper mapper;
    
     public CreateUserCommandTest(CommonTextFixture textFixture)
     {
         dbContext = textFixture.dbContext;
         mapper = textFixture.Mapper;
     }
    
    [Fact]
    public async void WhenAlreadyExistUserNumberIsGivenToCreateUser_ResponseMessageError_ShouldBeReturn()
    {
        // arrange( hazırlık)
            // Test aşamasında bir data yaratsın ve bitince silmesi için bunu yarattık
        
        var User = new User()
        {
            UserNumber = 100 ,
            Email = "testEmail@outlook.com",
            Password = "DenemeSifre123",
            FirstName = "Deneme",
            LastName = "Test",
            Role = "Admin",
            PasswordRetryCount = 3,
            LastActivityDate = new DateTime(2000,12,5)
        };

        dbContext.Add(User);
        dbContext.SaveChanges();
        
        // act(çalıştırma)
        var handler = new UserCommandHandler(mapper, dbContext);
        UserCreateRequest mapped = mapper.Map<UserCreateRequest>(User);
        var operation = new CreateUserCommand(mapped);
        var result = await handler.Handle(operation, default);
        
        // assert(dogrulama)
        result.Message.Should().Be("Error");
    }
}