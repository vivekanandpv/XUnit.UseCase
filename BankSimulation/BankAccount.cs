namespace BankSimulation
{
    public class BankAccount
    {
        public double Balance { get; private set; }

        public int AccountNumber { get; }

        public event EventHandler<BalanceChangingEventArgs> BalanceChanging;
        public event EventHandler<BalanceChangedEventArgs> BalanceChanged;

        public BankAccount(int accountNumber, double balance)
        {
            if (accountNumber < StaticDataProvider.MinAccountNumber ||
                accountNumber > StaticDataProvider.MaxAccountNumber)
            {
                throw new ArgumentException("Invalid account number");
            }

            if (balance < 0)
            {
                throw new ArgumentException("Invalid balance");
            }

            AccountNumber = accountNumber;
            Balance = balance;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Invalid amount for deposit");
            }

            BalanceChanging?.Invoke(this, new BalanceChangingEventArgs{CurrentBalance = Balance, NextBalance = Balance + amount});
            
            var previousBalance = Balance;
            Balance += amount;
            
            BalanceChanged?.Invoke(this, new BalanceChangedEventArgs(){CurrentBalance = Balance, PreviousBalance = previousBalance});
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0 || amount > Balance)
            {
                throw new ArgumentException("Invalid amount for withdrawal");
            }
            
            BalanceChanging?.Invoke(this, new BalanceChangingEventArgs{CurrentBalance = Balance, NextBalance = Balance - amount});

            var previousBalance = Balance;
            Balance -= amount;

            BalanceChanged?.Invoke(this, new BalanceChangedEventArgs(){CurrentBalance = Balance, PreviousBalance = previousBalance});
        }
    }
}