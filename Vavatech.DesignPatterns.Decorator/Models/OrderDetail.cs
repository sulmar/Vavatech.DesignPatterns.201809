namespace Vavatech.DesignPatterns.Decorator.Models
{
    public class OrderDetail
    {
        public OrderDetail(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;

            UnitPrice = product.UnitPrice;
        }

        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public decimal Amount => UnitPrice * Quantity;
    }

}
