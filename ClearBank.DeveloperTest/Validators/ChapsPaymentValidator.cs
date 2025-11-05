using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators;

public class ChapsPaymentValidator : IPaymentValidator
{
    public bool Validate(Account account, MakePaymentRequest request)
    {
        return account != null &&
               account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps) &&
               account.Status == AccountStatus.Live;
    }
}