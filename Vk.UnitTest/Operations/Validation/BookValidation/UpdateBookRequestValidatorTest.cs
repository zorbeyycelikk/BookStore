using FluentAssertions; 
using Vk.Operation.Validation;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.BookCommandTest;

public class UpdateBookRequestValidatorTest : IClassFixture<CommonTextFixture>
{
    [Theory]
    // Test BookNumber= ""
    [InlineData(0 , 300 , "Test")] 
    
    // Test PageCount= ""
    [InlineData(300 , 0 , "Test")] 
    
    // Test Publisher.length > 50
    [InlineData(300,300,"TesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTestt")] 
    
    public void WhenInvalidInputAreGivenToUpdateBook_Validator_ShouldBeReturnErrors(
        int BookNumber , int PageCount , string Publisher
        )
    {
        // arrange( hazırlık)
        BookUpdateRequest test = new BookUpdateRequest();
        test.BookNumber = BookNumber;
        test.PageCount = PageCount;
        test.Publisher = Publisher;
        
        // act(çalıştırma)
        UpdateBookValidator validator = new UpdateBookValidator();
        var result = validator.Validate(test);
        
        // assert(dogrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}