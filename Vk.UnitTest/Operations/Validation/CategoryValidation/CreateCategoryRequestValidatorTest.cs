using FluentAssertions; 
using Vk.Operation.Validation;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.CategoryCommandTest;

public class CreateCategoryRequestValidatorTest : IClassFixture<CommonTextFixture>
{
    [Theory]
    // Test CategoryName = ""
    [InlineData(1, "")] 
    // Test CategoryName.length > 50
    [InlineData(2, "TesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTestt")] 
    
    public void WhenInvalidInputAreGivenToCreateCategory_Validator_ShouldBeReturnErrors(
        int CategoryNumber , string CategoryName
        )
    {
        // arrange( hazırlık)
        CategoryCreateRequest test = new CategoryCreateRequest();
        test.CategoryNumber = CategoryNumber;
        test.CategoryName = CategoryName;
        
        // act(çalıştırma)
        CreateCategoryValidator validator = new CreateCategoryValidator();
        var result = validator.Validate(test);
        
        // assert(dogrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}