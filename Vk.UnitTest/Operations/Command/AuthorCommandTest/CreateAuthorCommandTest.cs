using AutoMapper;
using FluentAssertions;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.AuthorCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class CreateAuthorCommandTest : IClassFixture<CommonTextFixture>
{
     private readonly VkDbContext dbContext;
     private readonly IMapper mapper;
    
     public CreateAuthorCommandTest(CommonTextFixture textFixture)
     {
         dbContext = textFixture.dbContext;
         mapper = textFixture.Mapper;
     }
    
    [Fact]
    public async void WhenAlreadyExistAuthorNumberIsGivenToCreateAuthor_ResponseMessageError_ShouldBeReturn()
    {
        // arrange( hazırlık)
            // Test aşamasında bir data yaratsın ve bitince silmesi için bunu yarattık
            
        var author = new Author()
        {
            AuthorNumber = 100 , 
            Name = "John" , 
            Surname = "Doe" , 
            BirthDate = new DateTime(1976, 9, 18)
        };
        dbContext.Add(author);
        dbContext.SaveChanges();
        
        // act(çalıştırma)
        var handler = new AuthorCommandHandler(mapper , dbContext);
        AuthorCreateRequest mapped = mapper.Map<AuthorCreateRequest>(author);
        var operation = new CreateAuthorCommand(mapped);
        var result = await handler.Handle(operation, default);
        
        // assert(dogrulama)
        result.Message.Should().Be("Error");
    }
}