namespace BankSimulation.Tests
{
    public class BankAccountShould
    {
        [Trait(nameof(BankAccountShould), "Creation")]
        [Theory]
        [InlineData(1_000_000, 0.0)]
        [InlineData(10_000_000, 7845.0)]
        public void CreateInstanceWithValidAccountNumberAndBalance(int accountNumber, double initialBalance)
        {
            var sut = new BankAccount(accountNumber, initialBalance);

            Assert.NotNull(sut);
        }

        [Trait(nameof(BankAccountShould), "Creation")]
        [Theory]
        [InlineData(1451)]
        [InlineData(0)]
        [InlineData(10_000)]
        [InlineData(10_000_001)]
        public void ThrowArgumentExceptionForInvalidAccountNumber(int accountNumber)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var sut = new BankAccount(accountNumber, 474);
            });

            Assert.Equal("Invalid account number", ex.Message);
        }

        [Trait(nameof(BankAccountShould), "Creation")]
        [Theory]
        [InlineData(-100)]
        [InlineData(-7.2)]
        public void ThrowArgumentExceptionForNegativeInitialBalance(double balance)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var sut = new BankAccount(1_000_007, balance);
            });

            Assert.Equal("Invalid balance", ex.Message);
        }

        [Trait(nameof(BankAccountShould), "Creation")]
        [Fact]
        public void ReturnInitialBalanceThroughProperty()
        {
            var initialBalance = 85632.25;
            var sut = new BankAccount(1_000_007, initialBalance);

            Assert.Equal(initialBalance, sut.Balance, 2);
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Deposit))]
        [Theory]
        [InlineData(0, 145.25)]
        [InlineData(1000, 582.01)]
        [InlineData(41.50, 552_200_145.25)]
        public void UpdateBalanceByAmountForDeposit(double initialBalance, double amount)
        {
            var sut = new BankAccount(1_000_007, initialBalance);
            sut.Deposit(amount);
            Assert.Equal(initialBalance + amount, sut.Balance, 2);
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Deposit))]
        [Fact]
        public void RaiseBalanceChangingEventBeforeTheDeposit()
        {
            var initialBalance = 0;
            var depositAmount = 1000;
            var sut = new BankAccount(1_000_007, initialBalance);


            Assert.Raises<BalanceChangingEventArgs>(
                handler => sut.BalanceChanging += handler,
                handler => sut.BalanceChanging -= handler,
                () => sut.Deposit(1452.25)
                );
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Deposit))]
        [Fact]
        public void RaiseBalanceChangingEventBeforeDepositWithCorrectProperties()
        {
            var initialBalance = 0;
            var depositAmount = 1000;
            var sut = new BankAccount(1_000_007, initialBalance);


            var ea = Assert.Raises<BalanceChangingEventArgs>(
                handler => sut.BalanceChanging += handler,
                handler => sut.BalanceChanging -= handler,
                () => sut.Deposit(depositAmount)
            );

            Assert.Equal(initialBalance + depositAmount, ea.Arguments.NextBalance);
            Assert.Equal(initialBalance, ea.Arguments.CurrentBalance);
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Deposit))]
        [Fact]
        public void RaiseBalanceChangedEventAfterTheDeposit()
        {
            var initialBalance = 0;
            var depositAmount = 1000;
            var sut = new BankAccount(1_000_007, initialBalance);


            Assert.Raises<BalanceChangedEventArgs>(
                handler => sut.BalanceChanged += handler,
                handler => sut.BalanceChanged -= handler,
                () => sut.Deposit(1452.25)
            );
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Deposit))]
        [Fact]
        public void RaiseBalanceChangedEventAfterDepositWithCorrectProperties()
        {
            var initialBalance = 0;
            var depositAmount = 1000;
            var sut = new BankAccount(1_000_007, initialBalance);


            var ea = Assert.Raises<BalanceChangedEventArgs>(
                handler => sut.BalanceChanged += handler,
                handler => sut.BalanceChanged -= handler,
                () => sut.Deposit(depositAmount)
            );

            Assert.Equal(initialBalance + depositAmount, ea.Arguments.CurrentBalance);
            Assert.Equal(initialBalance, ea.Arguments.PreviousBalance);
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Withdraw))]
        [Theory]
        [InlineData(2000, 145.25)]
        [InlineData(1000, 582.01)]
        [InlineData(552_200_145.25, 52_525.25)]
        public void UpdateBalanceByAmountForWithdraw(double initialBalance, double amount)
        {
            var sut = new BankAccount(1_000_007, initialBalance);
            sut.Withdraw(amount);
            Assert.Equal(initialBalance - amount, sut.Balance, 2);
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Withdraw))]
        [Fact]
        public void RaiseBalanceChangingEventBeforeTheWithdraw()
        {
            var initialBalance = 5000;
            var depositAmount = 1000;
            var sut = new BankAccount(1_000_007, initialBalance);


            Assert.Raises<BalanceChangingEventArgs>(
                handler => sut.BalanceChanging += handler,
                handler => sut.BalanceChanging -= handler,
                () => sut.Withdraw(1452.25)
                );
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Withdraw))]
        [Fact]
        public void RaiseBalanceChangingEventBeforeWithdrawWithCorrectProperties()
        {
            var initialBalance = 5000;
            var withdrawAmount = 1000;
            var sut = new BankAccount(1_000_007, initialBalance);


            var ea = Assert.Raises<BalanceChangingEventArgs>(
                handler => sut.BalanceChanging += handler,
                handler => sut.BalanceChanging -= handler,
                () => sut.Withdraw(withdrawAmount)
            );

            Assert.Equal(initialBalance - withdrawAmount, ea.Arguments.NextBalance);
            Assert.Equal(initialBalance, ea.Arguments.CurrentBalance);
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Withdraw))]
        [Fact]
        public void RaiseBalanceChangedEventAfterTheWithdraw()
        {
            var initialBalance = 5000;
            var withdrawAmount = 1000;
            var sut = new BankAccount(1_000_007, initialBalance);


            Assert.Raises<BalanceChangedEventArgs>(
                handler => sut.BalanceChanged += handler,
                handler => sut.BalanceChanged -= handler,
                () => sut.Withdraw(1452.25)
            );
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Withdraw))]
        [Fact]
        public void RaiseBalanceChangedEventAfterWithdrawWithCorrectProperties()
        {
            var initialBalance = 5000;
            var withdrawAmount = 1000;
            var sut = new BankAccount(1_000_007, initialBalance);


            var ea = Assert.Raises<BalanceChangedEventArgs>(
                handler => sut.BalanceChanged += handler,
                handler => sut.BalanceChanged -= handler,
                () => sut.Withdraw(withdrawAmount)
            );

            Assert.Equal(initialBalance - withdrawAmount, ea.Arguments.CurrentBalance);
            Assert.Equal(initialBalance, ea.Arguments.PreviousBalance);
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Deposit))]
        [Theory]
        [InlineData(0.0)]
        [InlineData(-10.25)]
        public void ThrowArgumentExceptionForInvalidDeposit(double amount)
        {
            var sut = new BankAccount(1_000_007, 100);

            var ex = Assert.Throws<ArgumentException>(() => sut.Deposit(amount));
            Assert.Equal("Invalid amount for deposit", ex.Message);
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Withdraw))]
        [Theory]
        [InlineData(0.0)]
        [InlineData(-10.25)]
        [InlineData(4500.25)]
        public void ThrowArgumentExceptionForInvalidWithdraw(double amount)
        {
            var sut = new BankAccount(1_000_007, 100);

            var ex = Assert.Throws<ArgumentException>(() => sut.Withdraw(amount));
            Assert.Equal("Invalid amount for withdrawal", ex.Message);
        }

        [Trait(nameof(BankAccountShould), nameof(BankAccount.Withdraw))]
        [Fact]
        public void AllowZeroBalanceAfterWithdrawal()
        {
            var initialBalance = 100;
            var sut = new BankAccount(1_000_007, initialBalance);
            sut.Withdraw(initialBalance);
            Assert.Equal(0, sut.Balance);
        }
    }
}