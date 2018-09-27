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
            CustomerCalculatorTest();
            HappyHoursCalculatorTest();

            CustomerCalculatorObsoleteTest();
            HappyHoursCalculatorObsoleteTest();
        }

        private static void HappyHoursCalculatorTest()
        {
            Order order = CreateOrder();

            BaseOrderCalculator calculator = new HappyHoursOrderCalculator(
                    TimeSpan.FromHours(9.5),
                    TimeSpan.FromHours(17),
                    0.1m);

            calculator.CalculateDiscount(order);

            Console.WriteLine($"Total: {order.TotalAmount} " +
                $"Discount: {order.DiscountAmount} " +
                $"Final: {order.FinalAmount}");
        }

        private static void CustomerCalculatorTest()
        {
            Order order = CreateOrder();

            BaseOrderCalculator calculator = new CustomerOrderCalculator("a", 20m);

            calculator.CalculateDiscount(order);

            Console.WriteLine($"Total: {order.TotalAmount} " +
                $"Discount: {order.DiscountAmount} " +
                $"Final: {order.FinalAmount}");
        }

        private static void HappyHoursCalculatorObsoleteTest()
        {
            Order order = CreateOrder();

            IOrderCalculator calculator = new HappyHoursOrderCalculatorObsolete(
                    TimeSpan.FromHours(9.5),
                    TimeSpan.FromHours(17),
                    0.1m);

            calculator.CalculateDiscount(order);

            Console.WriteLine($"Total: {order.TotalAmount} " +
                $"Discount: {order.DiscountAmount} " +
                $"Final: {order.FinalAmount}");
        }

        private static void CustomerCalculatorObsoleteTest()
        {
            Order order = CreateOrder();

            IOrderCalculator calculator = new CustomerOrderCalculatorObsolete("a", 20m);

            calculator.CalculateDiscount(order);

            Console.WriteLine($"Total: {order.TotalAmount} " +
                $"Discount: {order.DiscountAmount} " +
                $"Final: {order.FinalAmount}");
        }

        private static Order CreateOrder()
        {
            Customer customer = new Customer("Anna", "Smith");

            Order order = new Order
            {
                OrderDate = DateTime.Parse("2018-09-27 9:30"),
                Customer = customer
            };


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
            return order;
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }

        public IList<OrderDetail> Details { get; set; } = new List<OrderDetail>();

        public decimal TotalAmount => Details.Sum(d => d.Amount);

        public decimal DiscountAmount { get; set; }

        public decimal FinalAmount => TotalAmount - DiscountAmount;

        public Order()
        {
            OrderDate = DateTime.Now;
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


    public class Customer
    {
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
