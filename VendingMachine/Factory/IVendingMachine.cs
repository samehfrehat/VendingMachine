using MoneySlots;
using Products;
using System.Collections.Generic;

namespace Factory
{
    public interface IVendingMachine 
    {
        bool AddProducts(IEnumerable<IProduct> products);

        Dictionary<(int, int), IProduct> GetProducts();

        void ValidateProductOption(string position);

        void AddPayment(double amount, string currency, MoneySlotTypes slotType);

        void ValidateExchange();

        void MakePayment();

        void DispenseProduct();
    }
}
