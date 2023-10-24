using FluentAssertions; 
using Vk.Operation.Validation;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.UserCommandTest;

public class CreateUserRequestValidatorTest : IClassFixture<CommonTextFixture>
{
    [Theory]
    // Test FirstName = ""
    [InlineData("", "Test" ,"Test" , "Test" , "Test")] 
    
    // Test FirstName.length > 50
    [InlineData("testttestttestttestttestttestttestttestttestttesttt", "Test" ,"Test" , "Test" , "Test")] 
    
    // Test LastName = ""
    [InlineData("Test", "" ,"Test" , "Test" , "Test")] 
    
    // Test LastName.length > 50
    [InlineData("Test", "testttestttestttestttestttestttestttestttestttesttt" ,"Test" , "Test" , "Test")] 
    
    // Test Email = ""
    [InlineData("Test", "Test" ,"" , "Test" , "Test")] 
    
    // Test Email.length > 50
    [InlineData("Test", "Test" ,"testttestttestttestttestttestttestttestttestttesttt" , "Test" , "Test")] 
    
    // Test Password = ""
    [InlineData("Test", "Test" ,"Test" , "" , "Test")] 
    
    // Test Password.length > 50
    [InlineData("Test", "Test" ,"Test" , "testttestttestttestttestttestttestttestttestttesttt" , "Test")] 
    
    // Test Role = ""
    [InlineData("Test", "Test" ,"Test" , "Test" , "")] 
    
    // Test Role.length > 10
    [InlineData("Test", "Test" ,"Test" , "Test" , "testttesttt")] 
    
    public void WhenInvalidInputAreGivenToCreateUser_Validator_ShouldBeReturnErrors(
        string FirstName , string LastName , string Email , string Password , string Role)
    {
        // arrange( hazırlık)
        UserCreateRequest test = new UserCreateRequest();
        test.FirstName = FirstName;
        test.LastName = LastName;
        test.Email = Email;
        test.Password = Password;
        test.Role = Role;
        // act(çalıştırma)
        CreateUserValidator validator = new CreateUserValidator();
        var result = validator.Validate(test);
        
        // assert(dogrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}