using MoneySlots;
using Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factory
{
    public sealed class SnackMachine : BaseVendingMachine, IVendingMachine
    {
        private SnackMachine() : base (5,5,VendingMachineTypes.Snaks)
        {
        }

        private static readonly Lazy<IVendingMachine> _snackMachine = new(() => new SnackMachine());

        private IProduct _product = null;

        public static IVendingMachine Instance => _snackMachine.Value;

        public bool AddProducts(IEnumerable<IProduct> products)
        {
            if(products.Count() > _rows * _colmns)
            {
                return false;
            }

            foreach (var item in products)
            {
                if (!productsPositions.TryAdd((item.Position.Item1, item.Position.Item2), item)) 
                {
                    return false;
                }
            }

            return true;
        }

        public Dictionary<(int,int),IProduct> GetProducts()
        {
            return productsPositions;
        }


        public void ValidateProductOption(string position)
        {
            if (string.IsNullOrWhiteSpace(position) ||
                position.Length != 2)
            {
                Console.WriteLine("Invalid Insert");
                return;
            }

            if (int.TryParse(position[0].ToString(), out var row) &&
                int.TryParse(position[1].ToString(), out var colmn))
            {
                _product = CheckIfProductAvailable((row, colmn));

                if (_product == null)
                {
                    Console.WriteLine("Product is not available");
                    return;
                }

                Console.WriteLine($"{_product.Type} price is: {_product.Price}");
            }
        }

        public void AddPayment(double amount, string currency, MoneySlotTypes slotType)
        {
            switch (slotType)
            {
                case MoneySlotTypes.CardSlot:
                    //payment gateway
                    break;
                case MoneySlotTypes.CoinSlot:

                    if (_coinSlot.AddMoney(amount, currency))
                    {
                        Console.WriteLine($"Total Payment: {_coinSlot.AvailableBalance() + _noteSlot.AvailableBalance()}");
                        break;
                    }

                    Console.WriteLine("Invalid Coin");
                    break;
                case MoneySlotTypes.NoteSlot:

                    if (_noteSlot.AddMoney(amount, currency))
                    {
                        Console.WriteLine($"Total Payment: {_coinSlot.AvailableBalance() + _noteSlot.AvailableBalance()}");
                        break;
                    }

                    Console.WriteLine("Invalid Note");
                    break;
            }
        }

        public void ValidateExchange()
        {
            var totalPayment = _noteSlot.AvailableBalance() + _coinSlot.AvailableBalance();

            if (_product.Price < totalPayment)
            {
                var remainingBalance = totalPayment - _product.Price;

                var change = MakeChange(remainingBalance);

                if (_noteSlot.HasEnoughChange(change[5], 50) &&
                    _noteSlot.HasEnoughChange(change[4], 20) &&
                    _coinSlot.HasEnoughChange(change[3], 1) &&
                    _coinSlot.HasEnoughChange(change[2], 0.5) &&
                    _coinSlot.HasEnoughChange(change[1], 0.2) &&
                    _coinSlot.HasEnoughChange(change[0], 0.1) )
                {
                    Console.WriteLine("Payment Accepted ...");
                    Console.WriteLine($"Please wait for the ${remainingBalance}");
                }
                else
                {
                    Console.WriteLine("There is not enough exchange");
                }

            }
            else if (_product.Price > totalPayment)
            {
                Console.WriteLine("Insufficient balance");
            }
            else
            {
                Console.WriteLine("Payment Accepted ...");
            }

        }

        private IProduct CheckIfProductAvailable((int, int) position)
        {
            if (productsPositions.TryGetValue(position, out var product) && product.Quantity > 0)
            {
                return product;
            }

            return null;
        }

        //coin change problem using greedy algorithm
        private static int[] MakeChange(double remainingBalance)
        {
            var coins = new int[6];
            var remainAmount = 0.0;

            if ((remainingBalance % 50) < remainingBalance)
            {
                coins[5] = (int)(remainingBalance / 50);
                remainAmount = remainingBalance % 50;
                remainingBalance = remainAmount;
            }
            if ((remainingBalance % 20) < remainingBalance)
            {
                coins[4] = (int)(remainingBalance / 20);
                remainAmount = remainingBalance % 20;
                remainingBalance = remainAmount;
            }
            if ((remainingBalance % 1) < remainingBalance)
            {
                coins[3] = (int)(remainingBalance / 1);
                remainAmount = remainingBalance % 1;
                remainingBalance = remainAmount;
            }
            if ((remainingBalance % 0.5) < remainingBalance)
            {
                coins[2] = (int)(remainingBalance / 0.5);
                remainAmount = remainingBalance % 0.5;
                remainingBalance = remainAmount;
            }
            if ((remainingBalance % 0.2) < remainingBalance)
            {
                coins[1] = (int)(remainingBalance / 0.2);
                remainAmount = remainingBalance % 0.2;
                remainingBalance = remainAmount;
            }
            if ((remainingBalance % 0.1) < remainingBalance)
            {
                coins[0] = (int)(remainingBalance / 0.1);
                remainAmount = remainingBalance % 0.1;
            }

            return coins;
        }
    }
}
