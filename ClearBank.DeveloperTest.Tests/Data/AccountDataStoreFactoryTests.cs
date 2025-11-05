using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Interfaces;

namespace ClearBank.DeveloperTest.Tests.Data;

public class AccountDataStoreFactoryTests
{
    private readonly Mock<IAccountDataStore> _mockMain = new();
    private readonly Mock<IAccountDataStore> _mockBackup = new();
    private readonly Mock<IConfiguration> _mockConfiguration = new();

    [Fact]
    public void GetDataStore_ShouldReturnMain_WhenDataStoreTypeIsNotBackup()
    {
        // Arrange
        _mockConfiguration.Setup(c => c["DataStoreType"]).Returns("Main");
        var factory = new AccountDataStoreFactory(_mockMain.Object, _mockBackup.Object, _mockConfiguration.Object);

        // Act
        var result = factory.GetDataStore();

        // Assert
        Assert.Equal(_mockMain.Object, result);
    }

    [Fact]
    public void GetDataStore_ShouldReturnBackup_WhenDataStoreTypeIsBackup()
    {
        // Arrange
        _mockConfiguration.Setup(c => c["DataStoreType"]).Returns(DataStoreType.Backup);
        var factory = new AccountDataStoreFactory(_mockMain.Object, _mockBackup.Object, _mockConfiguration.Object);

        // Act
        var result = factory.GetDataStore();

        // Assert
        Assert.Equal(_mockBackup.Object, result);
    }

    [Fact]
    public void GetDataStore_ShouldReturnMain_WhenDataStoreTypeIsNull()
    {
        // Arrange
        _mockConfiguration.Setup(c => c["DataStoreType"]).Returns((string?)null);
        var factory = new AccountDataStoreFactory(_mockMain.Object, _mockBackup.Object, _mockConfiguration.Object);

        // Act
        var result = factory.GetDataStore();

        // Assert
        Assert.Equal(_mockMain.Object, result);
    }

    [Fact]
    public void GetDataStore_ShouldReturnMain_WhenDataStoreTypeIsUnknown()
    {
        // Arrange
        _mockConfiguration.Setup(c => c["DataStoreType"]).Returns("UnknownType");
        var factory = new AccountDataStoreFactory(_mockMain.Object, _mockBackup.Object, _mockConfiguration.Object);

        // Act
        var result = factory.GetDataStore();

        // Assert
        Assert.Equal(_mockMain.Object, result);
    }
}
