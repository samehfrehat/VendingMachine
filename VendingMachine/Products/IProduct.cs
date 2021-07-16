namespace Products
{
    public interface IProduct
    {
        ProductTypes Type { get; set; }

        double Price { get; set; }

        (int,int) Position { get; set; }

        int Quantity { get; set; }
    }
}
