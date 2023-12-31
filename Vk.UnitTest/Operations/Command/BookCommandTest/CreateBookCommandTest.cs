using AutoMapper;
using FluentAssertions;
using MediatR;
using Vk.Base.Response;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Operation.Command;
using Vk.Operation.Cqrs;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.BookCommandTest;

//CommonTextFixture'nin sağlamış olduğu mapper ve dbcontext'e erişim sağlar
public class CreateBookCommandTest : IClassFixture<CommonTextFixture>
{
     private readonly VkDbContext dbContext;
     private readonly IMapper mapper;
    
     public CreateBookCommandTest(CommonTextFixture textFixture)
     {
         dbContext = textFixture.dbContext;
         mapper = textFixture.Mapper;
     }
    
    [Fact]
    public async void WhenAlreadyExistBookNumberIsGivenToCreateBook_ResponseMessageError_ShouldBeReturn()
    {
        // arrange( hazırlık)
            // Test aşamasında bir data yaratsın ve bitince silmesi için bunu yarattık
        
        var book = new Book()
        { Id = 100, InsertDate = DateTime.Now.AddDays(-3), UpdateDate = DateTime.Now.AddMinutes(-30), IsActive = true,
            BookNumber = 100, HeadLine = "WhenAlreadyExistBookNumberIsGiven_InvalidOperationException_ShouldBeReturn",
            PageCount = 300, Publisher = "Adventure Books Publishing", ISNB = "12345678904", AuthorId = 5, CategoryId = 7
        };
        dbContext.Add(book);
        dbContext.SaveChanges();
        
        // act(çalıştırma)
        var handler = new BookCommandHandler(mapper, dbContext);
        BookCreateRequest mapped = mapper.Map<BookCreateRequest>(book);
        var operation = new CreateBookCommand(mapped);
        var result = await handler.Handle(operation, default);
        
        // assert(dogrulama)
        result.Message.Should().Be("Error");
        
        /*Mantik tam olarak şu.
         * Arrange kısmında bir book nesnesi oluşturuyorum.Bunun amacı oluşturduğum inline db'deki veriye kendim fiziksel
         * olarak gidip bakmaktan kaçınmamız.
         * Yani burada yaptığımız bir nesne oluşturduk ve db'ye kaydettik.Fakat ben tekrardan bu nesneyi db'de create
         * etmeye çalışıyorum.Böyle olduğu zaman cqrs>commandHandler içinde alacağımız hata "Kitap zaten mevcut." hatasi.
         * Ve o yazılanı yakalamaya çalışıyoruz.
         */
    }
}