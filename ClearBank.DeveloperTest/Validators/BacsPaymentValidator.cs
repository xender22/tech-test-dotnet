using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators;

public class BacsPaymentValidator : IPaymentValidator
{
    public bool Validate(Account account, MakePaymentRequest request)
    {
        return account != null && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs);
    }
}