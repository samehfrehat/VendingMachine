namespace MoneySlots
{
    //Assuming that we have an integration with a payment gateway we can implement this class
    public class CardSlot : IMoneySlot
    {
        private readonly MoneySlotTypes _slotType = MoneySlotTypes.CardSlot;

        public bool AddMoney(double amount, string currency)
        {
            return true;
        }

        public double AvailableBalance()
        {
            return 0;
        }

        public bool HasEnoughChange(int count, double type)
        {
            return true;
        }

        public void FlushBalance()
        {
        }

        public bool SubtractMoney(double type, int count)
        {
            return true;
        }
    }
}
