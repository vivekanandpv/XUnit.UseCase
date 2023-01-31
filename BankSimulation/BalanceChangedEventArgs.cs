namespace BankSimulation;

public class BalanceChangedEventArgs : EventArgs
{
    public double PreviousBalance { get; set; }
    public double CurrentBalance { get; set; }
}