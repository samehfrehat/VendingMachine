using Factory;
using MoneySlots;
using Products;
using System;
using System.Collections.Generic;

namespace SnacksVM
{
    static class Program
    {
        private static readonly IVendingMachine _snackVM = SnackMachine.Instance;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello! Lets Build Somthing ...");

            if (!_snackVM.AddProducts(GetProducts())) {
                Console.WriteLine("Snacks Machine does not have enough positions");
                return;
            }

            Console.WriteLine("Please Insert Product Number: ");
            _snackVM.ValidateProductOption(Console.ReadLine());


            Console.WriteLine("Please Insert The Required Amount: ");
            _snackVM.AddPayment(0.1,"USD",MoneySlotTypes.CoinSlot);
            _snackVM.AddPayment(0.5, "USD", MoneySlotTypes.CoinSlot);
            _snackVM.AddPayment(0.2, "USD", MoneySlotTypes.CoinSlot);
            _snackVM.AddPayment(1, "USD", MoneySlotTypes.CoinSlot);
            _snackVM.AddPayment(20, "USD", MoneySlotTypes.NoteSlot);
            _snackVM.AddPayment(0.5, "USD", MoneySlotTypes.CoinSlot);

            _snackVM.ValidateExchange();
            _snackVM.MakePayment();
            _snackVM.DispenseProduct();
            Console.WriteLine("Dispenses the selected snack...");

            Console.WriteLine("We Are Done!");
        }

        private static IEnumerable<IProduct> GetProducts() =>        
             new List<IProduct> { 
                new Product { Position= (1,1), Price = 3.50, Quantity = 10, Type = ProductTypes.Cheetos},
                new Product { Position= (1,2), Price = 6.00, Quantity = 4, Type = ProductTypes.KitKat}
            };
        
    }
}
