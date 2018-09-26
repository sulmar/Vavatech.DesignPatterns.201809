using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {

            Order order = new Order();

            Product product = new Product
            {
                Id = 1,
                Name = "Mouse",
                UnitPrice = 100,
            };

            Product product2 = new Product
            {
                Id = 2,
                Name = "Keyboard",
                UnitPrice = 399m,
            };

            order.AddDetail(product, 5);
            order.AddDetail(product2, 3);

            Console.WriteLine(order.TotalAmount);
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public IList<OrderDetail> Details { get; set; } = new List<OrderDetail>();

        public decimal TotalAmount => Details.Sum(d => d.Amount);

        public Order()
        {
            OrderDate = DateTime.Today;
        }

        public void AddDetail(Product product, int quantity)
        {
            OrderDetail detail = new OrderDetail(product, quantity);

            this.Details.Add(detail);
        }
    }

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

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
