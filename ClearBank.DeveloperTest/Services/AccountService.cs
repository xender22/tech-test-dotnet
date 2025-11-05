using ClearBank.DeveloperTest.Data.Interfaces;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services;

public class AccountService(IAccountDataStoreFactory dataStoreFactory) : IAccountService
{
    private readonly IAccountDataStore _dataStore = dataStoreFactory.GetDataStore();
    
    public Account GetAccount(string accountNumber)
    {
        return _dataStore.GetAccount(accountNumber);
    }
    
    public void UpdateAccount(Account account)
    {
        _dataStore.UpdateAccount(account);
    }
    
    
}