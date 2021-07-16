using System.Collections.Generic;

namespace MoneySlots
{
    public class NoteSlot : IMoneySlot
    {
        private readonly Dictionary<double, int> _notesBox = new()
        {
            { 20.00, 10 },
            { 50.00, 10 }
        };

        private readonly MoneySlotTypes _slotType = MoneySlotTypes.NoteSlot;

        private double _balance = 0.0;

        public bool AddMoney(double amount, string currency)
        {
            if (_notesBox.ContainsKey(amount))
            {
                _notesBox[amount]++;
                _balance += amount;
                return true;
            }

            return false;
        }

        public double AvailableBalance() => _balance;

        public bool HasEnoughChange(int count, double type)
        {
            if (_notesBox.TryGetValue(type, out var noteCount))
            {
                return noteCount >= count;
            }

            return false;
        }
    }
}
