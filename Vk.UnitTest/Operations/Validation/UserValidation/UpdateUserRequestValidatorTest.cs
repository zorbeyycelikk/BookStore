using FluentAssertions; 
using Vk.Operation.Validation;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.UserCommandTest;

public class UpdateUserRequestValidatorTest : IClassFixture<CommonTextFixture>
{
    [Theory]
    // Test FirstName = ""
    [InlineData("", "TestLastName" , "Email" , "TestPassword")] 
    // Test FirstName.length > 50
    [InlineData("TesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTestt", "TestLastName" , "Email" , "TestPassword")] 
    
    // Test LastName = ""
    [InlineData("", "" , "Email" , "TestPassword")] 
    // Test LastName.length > 50
    [InlineData("Test", "TesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTestt" , "Email" , "TestPassword")] 
    
    // Test Email = ""
    [InlineData("Test", "TestLastName" , "" , "TestPassword")] 
    // Test Email.length > 50
    [InlineData("TEST", "TestLastName" , "TesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTestt" , "TestPassword")] 
    
    // Test Password = ""
    [InlineData("", "TestLastName" , "Email" , "")] 
    // Test Password.length > 50
    [InlineData("TesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTestt", "TestLastName" , "Email" , "TesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTestt")] 

    public void WhenInvalidInputAreGivenToUpdateUser_Validator_ShouldBeReturnErrors(
        string FirstName , string LastName ,string Email ,string Password
        )
    {
        // arrange( hazırlık)
        UserUpdateRequest test = new UserUpdateRequest();
        test.FirstName = FirstName;
        test.LastName = LastName;
        test.Email = Email;
        test.Password = Password;
        
        // act(çalıştırma)
        UpdateUserValidator validator = new UpdateUserValidator();
        var result = validator.Validate(test);
        
        // assert(dogrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}