using FluentAssertions; 
using Vk.Operation.Validation;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.BookCommandTest;

public class CreateBookRequestValidatorTest : IClassFixture<CommonTextFixture>
{
    [Theory]
    // Test BookNumber = 0
    [InlineData(0 , "Test BookNumber" ,300,"Test Yayin" , "1234567890123",1 , 1)]
    
    // Test Headline = ""
    [InlineData(1, "" ,300,"Test Headline" , "1234567890123",1 , 1)] 
    
    // Test PageCount = 0
    [InlineData(2, "Test PageCount" ,0,"Test Yayin" , "1234567890123",1 , 1)]
    
    // Test Publisher > 50
    [InlineData(3, "Test Publisher" ,300,"1234567890123123456789012312345678901231234567890123" , "1234567890123",1 , 1)]
    
    // Test ISNB > 13
    [InlineData(4, "Test ISNB" ,300,"Test Yayin" , "12345678901230",1 , 1)]
    
    public void WhenInvalidInputAreGivenToCreateBookRequest_Validator_ShouldBeReturnErrors(
        int BookNumber , string HeadLine ,  int PageCount , string Publisher , string ISNB 
        , int AuthorId , int CategoryId
        )
    {
        // arrange( hazırlık)
        BookCreateRequest test = new BookCreateRequest();
        test.BookNumber = BookNumber;
        test.HeadLine = HeadLine;
        test.PageCount = PageCount;
        test.Publisher = Publisher;
        test.ISNB = ISNB;
        test.AuthorId = AuthorId;
        test.CategoryId = CategoryId;

        // act(çalıştırma)
        CreateBookValidator validator = new CreateBookValidator();
        var result = validator.Validate(test);
        
        // assert(dogrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}