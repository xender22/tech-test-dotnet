using System;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Validators;

public class PaymentValidatorFactory : IPaymentValidatorFactory
{
    public IPaymentValidator GetValidator(PaymentScheme scheme)
    {
        return scheme switch
        {
            PaymentScheme.Bacs => new BacsPaymentValidator(),
            PaymentScheme.FasterPayments => new FasterPaymentsValidator(),
            PaymentScheme.Chaps => new ChapsPaymentValidator(),
            _ => throw new NotSupportedException("Payment scheme not supported")
        };
    }
}