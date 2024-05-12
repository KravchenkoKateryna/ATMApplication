# ATM WPF Application

## UI Design by consentantaneity (@ipz222mno), without frameworks

![photo_2024-05-12_20-15-10](https://github.com/ipz222mno/ATMApplication/assets/157833402/617f36c9-6ebd-4ef4-83e1-67a4ed664f94)



https://github.com/ipz222mno/ATMApplication/assets/157833402/b1859c2f-40aa-4cb7-9286-497025e3d13c



https://github.com/ipz222mno/ATMApplication/assets/157833402/14e835d2-7ec4-453b-b2d5-0a07190a058f



![image](https://github.com/ipz222mno/ATMApplication/assets/157833402/79fd89b8-8bfb-42fe-afa5-cf0315a00c9d)


![image](https://github.com/ipz222mno/ATMApplication/assets/157833402/2e24cc6c-340d-4438-b8dd-23f41c75abb4)


![image](https://github.com/ipz222mno/ATMApplication/assets/157833402/4985172f-682d-4d56-90da-3d5832af0cfa)


![image](https://github.com/ipz222mno/ATMApplication/assets/157833402/5b336dca-8a78-42c7-8b70-e15c63b52f86)


## Description

### Programming principles

#### ISP

The Interface Segregation Principle (ISP) states that clients should not be forced to depend on interfaces they do not use. Instead, interfaces should be segregated so that clients only depend on the methods they need. This helps avoid imposing dependencies on clients for functionality they do not use, making the system more flexible and easier to change. For example [ITransactionManager](./MVVM/Model/Template/ITransactionManager.cs).


#### SRP

Single responsibility principle is followed in the project, classes are only responsible for things they are meant to do. Classes in models do their job, for example, the [AutomatedTellerMachine](./MVVM/Model/AutomatedTellerMachine.cs) and [Account](./MVVM/Model/Account.cs) class have properties that should belong to it.


#### DRY 

Principle that advocates for reducing repetition of code within a system. It emphasizes the importance of code reusability by encouraging developers to avoid duplicating logic or information in multiple places. Following the DRY principle helps in maintaining consistency, reducing errors, and improving the maintainability of software. Repetition of code is avoided in all [classes](./MVVM/Models/); repeated sections of code are moved to separate methods. 


### Design patterns

#### Observer

[`bank.SuccessfulOperation += SuccessfulOperationHandler;`](./MVVM/Model/TransactionManager.cs#L29) - Here, [TransactionManager](./MVVM/Model/TransactionManager.cs) subscribes to the `SuccessfulOperation` event of the [Bank](./MVVM/Model/Bank.cs) object. This implements the observer pattern, where [TransactionManager](./MVVM/Model/TransactionManager.cs) (observer) subscribes to changes in [Bank](./MVVM/Model/Bank.cs) (subject).

#### Facade

```Database database = new ();``` - Creating a Database object can be interpreted as using a facade to interact with the database, hiding the complexity of database interaction behind a simple interface.

#### Strategy

The `TopUpMoney`, `SendMoney`, and `WithdrawMoney` methods in [TransactionManager](./MVVM/Model/TransactionManager.cs) represent different strategies for performing transactions (top-up, transfer, withdrawal). Each method implements its strategy for handling monetary operations based on the provided parameter.


#### Command

The `TopUpMoney`, `SendMoney`, and `WithdrawMoney` methods also serve as examples of commands, encapsulating user actions into objects with a specific interface (`ICommand`). This allows controlling the execution of operations and passing parameters between application components.

Uses `RelayCommand` classes and command handlers (`MenuCommand` and `AuthCommand`) in [MainViewModel](./MVVM/ViewModel/MainViewModel.cs) and in general in all [ViewModels](./MVVM/ViewModel/)
