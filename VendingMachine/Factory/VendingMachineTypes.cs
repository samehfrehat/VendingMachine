using System.ComponentModel;

namespace Factory
{
    public enum VendingMachineTypes
    {
        [Description("Snaks Vending Machine")]
        Snaks = 1,

        [Description("Coffee Vending Machine")]
        Coffee = 2,
        
        [Description("Ice Cream Vending Machine")]
        IceCream = 3,
    }
}
