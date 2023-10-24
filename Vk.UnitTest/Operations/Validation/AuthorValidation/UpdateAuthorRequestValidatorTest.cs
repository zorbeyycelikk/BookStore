using FluentAssertions; 
using Vk.Operation.Validation;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command;

public class UpdateAuthorRequestValidatorTest : IClassFixture<CommonTextFixture>
{
    [Theory]
    // Test Name= ""
    [InlineData("", "Test")] 
    
    // Test Surname= ""
    [InlineData("Test" , "")] 
    
    // Test Name.length > 20
    [InlineData("TesttTesttTesttTesttt","Test")] 
    
    // Test Surname.length > 20
    [InlineData("Test" , "TesttTesttTesttTesttt")] 

    public void WhenInvalidInputAreGivenToUpdateAuthor_Validator_ShouldBeReturnErrors(
        string Name , string Surname 
        )
    {
        // arrange( hazırlık)
        AuthorUpdateRequest test = new AuthorUpdateRequest();
        test.Name = Name;
        test.Surname = Surname;
        
        // act(çalıştırma)
        UpdateAuthorValidator validator = new UpdateAuthorValidator();
        var result = validator.Validate(test);
        
        // assert(dogrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}