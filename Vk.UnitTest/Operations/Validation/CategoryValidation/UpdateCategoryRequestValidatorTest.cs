using FluentAssertions; 
using Vk.Operation.Validation;
using Vk.Schema;

namespace Vk.UnitTest.Operations.Command.CategoryCommandTest;

public class UpdateCategoryRequestValidatorTest : IClassFixture<CommonTextFixture>
{
    [Theory]
    // Test CategoryName = ""
    [InlineData("")] 
    // Test CategoryName.length > 50
    [InlineData("TesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTesttTestt")] 
    
    public void WhenInvalidInputAreGivenToUpdateCategory_Validator_ShouldBeReturnErrors(
        string CategoryName
        )
    {
        // arrange( hazırlık)
        CategoryUpdateRequest test = new CategoryUpdateRequest();
        test.CategoryName = CategoryName;
        
        // act(çalıştırma)
        UpdateCategoryValidator validator = new UpdateCategoryValidator();
        var result = validator.Validate(test);
        
        // assert(dogrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}