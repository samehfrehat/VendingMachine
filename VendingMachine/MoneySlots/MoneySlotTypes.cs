using System.ComponentModel;

namespace MoneySlots
{
    public enum MoneySlotTypes
    {
        [Description("Coin Slot")]
        CoinSlot = 1,

        [Description("Card Slot")]
        CardSlot = 2,

        [Description("Note Slot")]
        NoteSlot = 3,
    }
}
