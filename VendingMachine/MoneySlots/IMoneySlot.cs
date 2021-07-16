namespace MoneySlots
{
    public interface IMoneySlot
    {
        double AvailableBalance();

        bool AddMoney(double amount, string currency);

        bool SubtractMoney(double type, int count);

        bool HasEnoughChange(int count, double type);

        void FlushBalance();
    }
}
