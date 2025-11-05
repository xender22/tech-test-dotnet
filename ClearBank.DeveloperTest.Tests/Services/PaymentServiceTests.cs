using Xunit;
using Moq;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Validators.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.Services;

public class PaymentServiceTests
{
    private readonly Mock<IAccountService> _mockAccountService = new();
    private readonly Mock<IPaymentValidatorFactory> _mockValidatorFactory = new();
    private readonly PaymentService _paymentService;

    public PaymentServiceTests()
    {
        _paymentService = new PaymentService(_mockAccountService.Object, _mockValidatorFactory.Object);
    }

    [Fact]
    public void MakePayment_ShouldReturnFailure_WhenRequestIsNull()
    {
        // Act
        var result = _paymentService.MakePayment(null);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Request is null", result.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void MakePayment_ShouldReturnFailure_WhenAmountIsNonPositive(decimal amount)
    {
        // Arrange
        var request = new MakePaymentRequest
        {
            Amount = amount,
            CreditorAccountNumber = "ACC1",
            PaymentScheme = PaymentScheme.Bacs
        };

        // Act
        var result = _paymentService.MakePayment(request);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Amount must be greater than zero", result.Message);
    }

    [Fact]
    public void MakePayment_ShouldReturnFailure_WhenAccountNotFound()
    {
        // Arrange
        var request = new MakePaymentRequest
        {
            Amount = 100,
            CreditorAccountNumber = "ACC1",
            PaymentScheme = PaymentScheme.Bacs
        };
        _mockAccountService.Setup(s => s.GetAccount(request.CreditorAccountNumber)).Returns((Account?)null);

        // Act
        var result = _paymentService.MakePayment(request);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Account not found", result.Message);
    }

    [Fact]
    public void MakePayment_ShouldReturnFailure_WhenValidatorFails()
    {
        // Arrange
        var request = new MakePaymentRequest
        {
            Amount = 100,
            CreditorAccountNumber = "ACC1",
            PaymentScheme = PaymentScheme.Bacs
        };
        var account = new Account { AccountNumber = request.CreditorAccountNumber};
        account.Deposit(500m);
        _mockAccountService.Setup(s => s.GetAccount(request.CreditorAccountNumber)).Returns(account);

        var mockValidator = new Mock<IPaymentValidator>();
        mockValidator.Setup(v => v.Validate(account, request)).Returns(false);

        _mockValidatorFactory.Setup(f => f.GetValidator(request.PaymentScheme)).Returns(mockValidator.Object);

        // Act
        var result = _paymentService.MakePayment(request);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Payment not valid", result.Message);
    }

    [Fact]
    public void MakePayment_ShouldSucceed_WhenPaymentIsValid()
    {
        // Arrange
        var request = new MakePaymentRequest
        {
            Amount = 100,
            CreditorAccountNumber = "ACC1",
            PaymentScheme = PaymentScheme.Bacs
        };
        var account = new Account { AccountNumber = request.CreditorAccountNumber};
        account.Deposit(500m);
        _mockAccountService.Setup(s => s.GetAccount(request.CreditorAccountNumber)).Returns(account);

        var mockValidator = new Mock<IPaymentValidator>();
        mockValidator.Setup(v => v.Validate(account, request)).Returns(true);

        _mockValidatorFactory.Setup(f => f.GetValidator(request.PaymentScheme)).Returns(mockValidator.Object);

        // Act
        var result = _paymentService.MakePayment(request);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Payment successful", result.Message);
        Assert.Equal(400, account.Balance);
        _mockAccountService.Verify(s => s.UpdateAccount(account), Times.Once);
    }
}
