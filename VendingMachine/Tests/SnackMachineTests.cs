using Factory;
using MoneySlots;
using Products;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class SnackMachineTests
    {
        // Many test cases can be implemented this is an example of how it should be
      
        private readonly IVendingMachine _machine;

        public SnackMachineTests()
        {
            // do mocking for needed services
            _machine = SnackMachine.Instance;
        }

        [Fact]
        public void Add_VendingMachineProduct_ShouldReturnTrue()
        {
            //Arrange
            var Products = new List<IProduct> { new Product { Position = (1,2), Price = 1, Quantity = 1, Type = ProductTypes.Cheetos} };
          

            //Act
            var result = _machine.AddProducts(Products);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Add_VendingMachineProductToTheSamePosition_ShouldReturnFalse()
        {
            //Arrange
            var Products = new List<IProduct> { new Product { Position = (1, 1), Price = 1, Quantity = 1, Type = ProductTypes.Cheetos },
            new Product { Position = (1, 1), Price = 1, Quantity = 1, Type = ProductTypes.Cheetos }};

            //Act
            var result = _machine.AddProducts(Products);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Get_VendingMachineProductsWithPositions_ShouldReturnFalse()
        {
            //Arrange
            var Products = new List<IProduct> { new Product { Position = (2, 1), Price = 1, Quantity = 1, Type = ProductTypes.Cheetos },
            new Product { Position = (2, 2), Price = 1, Quantity = 1, Type = ProductTypes.Cheetos }};
            _machine.AddProducts(Products);

            //Act
            var result = _machine.GetProducts();

            //Assert
            Assert.True(result.ContainsKey((2,1)));
            Assert.True(result.ContainsKey((2,2)));
        }

        [Fact]
        public void ValidateProductOption_ShouldReturnTrue()
        {
            //Arrange
            var Products = new List<IProduct> { new Product { Position = (2, 1), Price = 1, Quantity = 1, Type = ProductTypes.Cheetos },
            new Product { Position = (2, 2), Price = 1, Quantity = 1, Type = ProductTypes.Cheetos }};
            _machine.AddProducts(Products);

            //Act
            var result = _machine.ValidateProductOption("21");

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ValidateProductOptionWithWrongPosition_ShouldReturnFalse()
        {
            //Act
            var result = _machine.ValidateProductOption("55");

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddInvalidCoin_ShouldReturnFalse()
        {
            //Act
            var result = _machine.AddPayment(5,"USD",MoneySlotTypes.CoinSlot);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddValidCoin_ShouldReturnTrue()
        {
            //Act
            var result = _machine.AddPayment(1, "USD", MoneySlotTypes.CoinSlot);

            //Assert
            Assert.True(result);
        }
    }
}
