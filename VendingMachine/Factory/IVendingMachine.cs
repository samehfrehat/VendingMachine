using MoneySlots;
using Products;
using System.Collections.Generic;

namespace Factory
{
    public interface IVendingMachine 
    {
        bool AddProducts(IEnumerable<IProduct> products);

        Dictionary<(int, int), IProduct> GetProducts();

        bool ValidateProductOption(string position);

        bool AddPayment(double amount, string currency, MoneySlotTypes slotType);

        void ValidateExchange();

        void MakePayment();

        void DispenseProduct();
    }
}
