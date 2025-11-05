using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators;

public class FasterPaymentsValidatorTests
{
    private readonly FasterPaymentsValidator _validator = new();

    [Fact]
        public void Validate_ShouldReturnFalse_WhenAccountIsNull()
        {
            // Arrange
            var request = new MakePaymentRequest { Amount = 100m };

            // Act
            var result = _validator.Validate(null, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenAccountDoesNotAllowFasterPayments()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs
            };
            account.Deposit(200m);
            var request = new MakePaymentRequest { Amount = 100m };

            // Act
            var result = _validator.Validate(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenAccountBalanceIsLessThanRequestAmount()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };
            account.Deposit(50m);
            var request = new MakePaymentRequest { Amount = 100m };

            // Act
            var result = _validator.Validate(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenAccountAllowsFasterPaymentsAndHasSufficientBalance()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments
            };
            account.Deposit(200m);
            var request = new MakePaymentRequest { Amount = 100m };

            // Act
            var result = _validator.Validate(account, request);

            // Assert
            Assert.True(result);
        }
}