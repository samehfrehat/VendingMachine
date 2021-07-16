namespace Products
{
    public struct Product : IProduct
    {
        public Product(ProductTypes type, double price, (int,int) position, int quantity)
        {
            Type = type;
            Price = price;
            Position = position;
            Quantity = quantity;
        }

        public ProductTypes Type { get; set; }

        public double Price { get; set; }

        public (int, int) Position { get; set; }

        public int Quantity { get; set; }
    }
}
