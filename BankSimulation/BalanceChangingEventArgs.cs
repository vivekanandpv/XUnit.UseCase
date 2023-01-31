namespace BankSimulation;

public class BalanceChangingEventArgs : EventArgs
{
    public double CurrentBalance { get; set; }
    public double NextBalance { get; set; }
}