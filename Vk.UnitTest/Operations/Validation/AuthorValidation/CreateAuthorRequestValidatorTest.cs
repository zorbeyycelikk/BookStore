using FluentAssertions; 
using Vk.Operation.Validation;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.AuthorCommandTest;

public class CreateAuthorRequestValidatorTest : IClassFixture<CommonTextFixture>
{
    [Theory]
    // Test Name = ""
    [InlineData(1, "" ,"Test")] 
    
    // Test Surname = ""
    [InlineData(2, "Test" ,"")] 
    
    // Test Name.length > 20
    [InlineData(3, "TesttTesttTesttTesttt" ,"Test")] 
    
    // Test Surname.length > 20
    [InlineData(4, "Test" ,"TesttTesttTesttTesttt")] 
    
    public void WhenInvalidInputAreGivenToCreateAuthor_Validator_ShouldBeReturnErrors(
        int AuthorNumber , string Name , string Surname
        )
    {
        // arrange( hazırlık)
        AuthorCreateRequest test = new AuthorCreateRequest();
        test.AuthorNumber = AuthorNumber;
        test.Name = Name;
        test.Surname = Surname;
        
        // act(çalıştırma)
        CreateAuthorValidator validator = new CreateAuthorValidator();
        var result = validator.Validate(test);
        
        // assert(dogrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}