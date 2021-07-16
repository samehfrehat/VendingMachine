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

        private readonly int[] _change = new int[6] { 0,0,0,0,0,0};

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

                MakeChange(remainingBalance);

                if (_noteSlot.HasEnoughChange(_change[5], 50) &&
                    _noteSlot.HasEnoughChange(_change[4], 20) &&
                    _coinSlot.HasEnoughChange(_change[3], 1) &&
                    _coinSlot.HasEnoughChange(_change[2], 0.5) &&
                    _coinSlot.HasEnoughChange(_change[1], 0.2) &&
                    _coinSlot.HasEnoughChange(_change[0], 0.1) )
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

        public void MakePayment()
        {
            if(_change[5] > 0)
            {
                _noteSlot.SubtractMoney(50, _change[5]);
            }
            if (_change[4] > 0)
            {
                _noteSlot.SubtractMoney(20, _change[4]);
            }
            if (_change[3] > 0)
            {
                _coinSlot.SubtractMoney(1, _change[3]);
            }
            if (_change[2] > 0)
            {
                _coinSlot.SubtractMoney(0.5, _change[2]);
            }
            if (_change[1] > 0)
            {
                _coinSlot.SubtractMoney(0.2, _change[1]);
            }
            if (_change[0] > 0)
            {
                _coinSlot.SubtractMoney(0.1, _change[0]);
            }
        }

        //here we need to decrease the quantity and reset all the used variables 
        public void DispenseProduct()
        {
            productsPositions[(_product.Position.Item1, _product.Position.Item2)].Quantity--;
            _coinSlot.FlushBalance();
            _noteSlot.FlushBalance();
            _cardSlot.FlushBalance();
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
        private void MakeChange(double remainingBalance)
        {
            if ((remainingBalance % 50) < remainingBalance)
            {
                _change[5] = (int)(remainingBalance / 50);
                remainingBalance %= 50;
            }
            if ((remainingBalance % 20) < remainingBalance)
            {
                _change[4] = (int)(remainingBalance / 20);
                remainingBalance %= 20;
            }
            if ((remainingBalance % 1) < remainingBalance)
            {
                _change[3] = (int)(remainingBalance / 1);
                remainingBalance %= 1;
            }
            if ((remainingBalance % 0.5) < remainingBalance)
            {
                _change[2] = (int)(remainingBalance / 0.5);
                remainingBalance %= 0.5;
            }
            if ((remainingBalance % 0.2) < remainingBalance)
            {
                _change[1] = (int)(remainingBalance / 0.2);
                remainingBalance %= 0.2;
            }
            if ((remainingBalance % 0.1) < remainingBalance)
            {
                _change[0] = (int)(remainingBalance / 0.1);
            }
        }
    }
}
