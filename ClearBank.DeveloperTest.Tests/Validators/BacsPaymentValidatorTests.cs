using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators;

public class BacsPaymentValidatorTests
{
    private readonly BacsPaymentValidator _validator = new();

    [Fact]
    public void Validate_ReturnsTrue_WhenAccountSupportsBacs()
    {
        // Arrange
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
        };
        var request = new MakePaymentRequest();

        // Act
        var result = _validator.Validate(account, request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Validate_ReturnsFalse_WhenAccountDoesNotSupportBacs()
    {
        // Arrange
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
        };
        var request = new MakePaymentRequest();

        // Act
        var result = _validator.Validate(account, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Validate_ReturnsFalse_WhenAccountWithNoFlagsSet()
    {
        // Arrange
        var account = new Account();
        var request = new MakePaymentRequest();

        // Act
        var result = _validator.Validate(account, request);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Validate_ReturnsFalse_WhenAccountIsNull()
    {
        // Arrange
        var request = new MakePaymentRequest();

        // Act
        var result = _validator.Validate(null, request);

        // Assert
        Assert.False(result);
    }
}