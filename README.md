### Test Description
In the 'PaymentService.cs' file you will find a method for making a payment. At a high level the steps for making a payment are:

 - Lookup the account the payment is being made from
 - Check the account is in a valid state to make the payment
 - Deduct the payment amount from the account's balance and update the account in the database
 
What we’d like you to do is refactor the code with the following things in mind:  
 - Adherence to SOLID principals
 - Testability  
 - Readability 

We’d also like you to add some unit tests to the ClearBank.DeveloperTest.Tests project to show how you would test the code that you’ve produced. The only specific ‘rules’ are:  

 - The solution should build.
 - The tests should all pass.
 - You should not change the method signature of the MakePayment method.

You are free to use any frameworks/NuGet packages that you see fit.  
 
You should plan to spend around 1 to 3 hours to complete the exercise.

## Assumptions

I've made the following assumptions while implementing the changes:

- The logic of `MakePayment` is **correct**, only the code structure required changes or improvements.

- The project was provided as two class libraries, and based on the `AccountDataStore` implementation (where some code was omitted for **brevity**), no further implementation is required beyond what was originally given.

- Following the first point, if the `MakePayment` logic is correct, then the `Validators` for payments can allow exceptions, for example, an account may have a transaction validated even if it is inactive (e.g., payment liabilities), or an account may make a payment without having the full balance (e.g., overdraft).



## My Improvements

In refactoring the code, I made the following changes:

- Moved the account operations to their own service (Single Responsibility Principle).  
- Moved the `AccountDataStore` into `AccountService`, as it is related to the account, and used the Factory pattern to select the configured one (Main or Backup).  
- Created multiple payment validators and instantiated the appropriate one through a Factory pattern. All validators implement a common interface that defines the `Validate` function.  
- Added null checks, made the `Balance` setter in the `Account` class private, and implemented internal `Withdraw` and `Deposit` functions for managing the balance.  
- Added unit tests for services, data, and validators.


## If I Had More Time

If I had more time, I would have made the operations asynchronous, added logging, and included an actual data source and project files for manual testing (such as an API controller, a repository implementation, and possibly a front end or a Swagger/OpenAPI setup).


## High Level Overview of Refactoring Logic

![alt text](image.png)

