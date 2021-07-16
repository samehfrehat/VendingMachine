using System.ComponentModel;

namespace Products
{
    public enum ProductTypes
    {
        [Description("Smuckers")]
        Smuckers = 1,

        [Description("Cheez-It")]
        CheezIt = 2,
        
        [Description("Cheetos")]
        Cheetos = 3,
        
        [Description("Kit Kat")]
        KitKat = 4,
        
        [Description("M&M's")]
        MMS = 5,
    }
}
