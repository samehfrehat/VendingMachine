using System.Collections.Generic;

namespace MoneySlots
{
    public class CoinSlot : IMoneySlot
    {
        private readonly Dictionary<double, int> _coinsBox = new() {
            { 0.1 , 100},
            { 0.2, 100 },
            { 0.5, 100 },
            { 1.00, 100 }
        };

        private readonly MoneySlotTypes _slotType = MoneySlotTypes.CoinSlot;

        private double _balance = 0.0;

        public bool AddMoney(double amount, string currency)
        {
            if (currency != "USD") return false;

            if (_coinsBox.ContainsKey(amount))
            {
                _coinsBox[amount]++;
                _balance += amount;
                return true;
            }

            return false;
        }

        public double AvailableBalance() => _balance;

        public void FlushBalance()
        {
            _balance = 0;
        }

        public bool HasEnoughChange(int count, double type)
        {
            if(_coinsBox.TryGetValue(type, out var coinCount))
            {
                return coinCount >= count;
            }

            return false;
        }

        public bool SubtractMoney(double type, int count)
        {
            if (_coinsBox.ContainsKey(type))
            {
                _coinsBox[type] -= count;
                return true;
            }

            return false;
        }
    }
}
