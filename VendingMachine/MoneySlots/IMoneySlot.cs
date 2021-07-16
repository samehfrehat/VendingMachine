namespace MoneySlots
{
    public interface IMoneySlot
    {
        double AvailableBalance();

        bool AddMoney(double amount, string currency);

        bool HasEnoughChange(int count, double type);
    }
}
