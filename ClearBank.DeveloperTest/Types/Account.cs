namespace ClearBank.DeveloperTest.Types
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; private set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }


        public decimal Withdraw(decimal amount)
        {
            return Balance -= amount;
        }
        
        public decimal Deposit(decimal amount)
        {
            return Balance += amount;
        }
    }
}
