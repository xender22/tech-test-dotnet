using Xunit;
using Moq;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.Services;

public class AccountServiceTests
{
    private readonly Mock<IAccountDataStoreFactory> _mockDataStoreFactory = new();
    private readonly Mock<IAccountDataStore> _mockDataStore = new();
    private readonly AccountService _accountService;

    public AccountServiceTests()
    {
        // Factory returns the mocked data store
        _mockDataStoreFactory.Setup(f => f.GetDataStore()).Returns(_mockDataStore.Object);

        // Initialize service
        _accountService = new AccountService(_mockDataStoreFactory.Object);
    }

    [Fact]
    public void GetAccount_ShouldReturnAccount_FromDataStore()
    {
        // Arrange
        var accountNumber = "ACC123";
        var expectedAccount = new Account { AccountNumber = accountNumber};
        expectedAccount.Deposit(100m);
        _mockDataStore.Setup(ds => ds.GetAccount(accountNumber)).Returns(expectedAccount);

        // Act
        var result = _accountService.GetAccount(accountNumber);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedAccount.AccountNumber, result.AccountNumber);
        Assert.Equal(expectedAccount.Balance, result.Balance);
        _mockDataStore.Verify(ds => ds.GetAccount(accountNumber), Times.Once);
    }

    [Fact]
    public void UpdateAccount_ShouldCallUpdateAccount_OnDataStore()
    {
        // Arrange
        var account = new Account { AccountNumber = "ACC123"};
        account.Deposit(100m);
        
        // Act
        _accountService.UpdateAccount(account);

        // Assert
        _mockDataStore.Verify(ds => ds.UpdateAccount(account), Times.Once);
    }
}