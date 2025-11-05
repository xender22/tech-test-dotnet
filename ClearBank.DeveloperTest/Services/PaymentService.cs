using System;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System.Configuration;
using ClearBank.DeveloperTest.Services.Interfaces;
using ClearBank.DeveloperTest.Validators.Interfaces;

namespace ClearBank.DeveloperTest.Services;

public class PaymentService(
    IAccountService accountService,
    IPaymentValidatorFactory paymentValidatorFactory) : IPaymentService
{
    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
        var result = new MakePaymentResult { };
        if (request == null)
        {
            result.Message = "Request is null";
            result.Success = false;
            return result;
        }

        if (request.Amount <= 0)
        {
            result.Message = "Amount must be greater than zero";
            result.Success = false;
            return result;
        }

        try
        {
            var account = accountService.GetAccount(request.CreditorAccountNumber);
            if (account == null)
            {
                result.Message = "Account not found";
                result.Success = false;
                return result;
            }

            var validator = paymentValidatorFactory.GetValidator(request.PaymentScheme);
            if (validator == null)
            {
                result.Message = "Payment scheme not supported";
                result.Success = false;
                return result;
            }

            var validateRequest = validator.Validate(account, request);
            if (!validateRequest)
            {
                result.Message = "Payment not valid";
                result.Success = false;
                return result;
            }


            account.Withdraw(request.Amount);
            accountService.UpdateAccount(account);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        result.Success = true;
        result.Message = "Payment successful";
        return result;
    }
}