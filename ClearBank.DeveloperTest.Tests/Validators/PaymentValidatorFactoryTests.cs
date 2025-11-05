using System;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators;

public class PaymentValidatorFactoryTests
{
    private readonly PaymentValidatorFactory _factory = new();

    [Theory]
    [InlineData(PaymentScheme.Bacs, typeof(BacsPaymentValidator))]
    [InlineData(PaymentScheme.FasterPayments, typeof(FasterPaymentsValidator))]
    [InlineData(PaymentScheme.Chaps, typeof(ChapsPaymentValidator))]
    public void GetValidator_Returns_CorrectValidator_ForSupportedSchemes(PaymentScheme scheme, Type expectedType)
    {
        // Act
        var validator = _factory.GetValidator(scheme);

        // Assert
        Assert.NotNull(validator);
        Assert.IsType(expectedType, validator);
    }

    [Fact]
    public void GetValidator_Throws_NotSupportedException_ForUnsupportedScheme()
    {
        // Arrange
        var unsupportedScheme = (PaymentScheme) 999;

        // Act
        var exception = Assert.Throws<NotSupportedException>(() => _factory.GetValidator(unsupportedScheme));
        
        // Assert
        Assert.Equal("Payment scheme not supported", exception.Message);
    }
}