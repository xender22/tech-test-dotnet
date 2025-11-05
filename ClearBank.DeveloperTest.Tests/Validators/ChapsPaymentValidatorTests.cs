using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Validators;

public class ChapsPaymentValidatorTests
{
    private readonly ChapsPaymentValidator _validator = new();

    [Fact]
        public void Validate_ShouldReturnFalse_WhenAccountIsNull()
        {
            // Arrange
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.Validate(null, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenAccountDoesNotAllowChaps()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Status = AccountStatus.Live
            };
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.Validate(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldReturnFalse_WhenAccountStatusIsNotLive()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Disabled
            };
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.Validate(account, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ShouldReturnTrue_WhenAccountAllowsChapsAndIsLive()
        {
            // Arrange
            var account = new Account
            {
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Live
            };
            var request = new MakePaymentRequest();

            // Act
            var result = _validator.Validate(account, request);

            // Assert
            Assert.True(result);
        }
}