using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Validators.Interfaces;

public interface IPaymentValidator
{
    bool Validate(Account account, MakePaymentRequest request);
}
