using ClearBank.DeveloperTest.Data.Interfaces;
using Microsoft.Extensions.Configuration;
namespace ClearBank.DeveloperTest.Data;

public class AccountDataStoreFactory(
    IAccountDataStore main,
    IAccountDataStore backup,
    IConfiguration configuration) : IAccountDataStoreFactory
{
    public IAccountDataStore GetDataStore()
    {
        var dataStoreType = configuration["DataStoreType"];
        return dataStoreType switch
        {
            DataStoreType.Backup => backup,
            _ => main
        };
    }
}
