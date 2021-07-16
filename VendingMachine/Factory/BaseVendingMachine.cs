using MoneySlots;
using Products;
using System.Collections.Generic;

namespace Factory
{
    public abstract class BaseVendingMachine
    {
        protected BaseVendingMachine(int rows, int colmns, VendingMachineTypes type)
        {
            _rows = rows;
            _colmns = colmns;
            _type = type;
            _coinSlot = new CoinSlot();
            _noteSlot = new NoteSlot();
            _cardSlot = new CardSlot();
        }

        protected readonly int _rows;

        protected readonly int _colmns;

        protected readonly Dictionary<(int,int), IProduct> productsPositions = new();

        protected readonly VendingMachineTypes _type;

        protected readonly IMoneySlot _coinSlot;

        protected readonly IMoneySlot _noteSlot;

        protected readonly IMoneySlot _cardSlot;
    }
}
