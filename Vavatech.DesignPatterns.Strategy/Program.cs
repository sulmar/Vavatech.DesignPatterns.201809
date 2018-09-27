using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = CreateOrder();

            // OrderCalculatorTestObsolete(order);

            ICanDiscountStrategy canDiscountStrategy = new HappyHoursCanDiscountStrategy(TimeSpan.FromHours(9.5), TimeSpan.FromHours(17));
            IApplyDiscountStrategy discountStrategy = new PercentageApplyDiscountStrategy(0.1m);
                
            OrderCalculator orderCalculator = 
                new OrderCalculator(canDiscountStrategy, discountStrategy);

            orderCalculator.CalculateDiscount(order);



            Console.WriteLine($"Total: {order.TotalAmount} " +
                $"Discount: {order.DiscountAmount} " +
                $"Final: {order.FinalAmount}");
        }

        private static void OrderCalculatorTestObsolete(Order order)
        {
            IDiscountStrategy discountStrategy =
                            new HappyHoursDiscountStrategy(TimeSpan.FromHours(9.5), TimeSpan.FromHours(17), 0.1m);

            OrderCalculatorObsolete calculator = new OrderCalculatorObsolete(discountStrategy);

            calculator.CalculateDiscount(order);
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



    public interface ICanDiscountStrategy
    {
        bool CanDiscount(Order order);
    }

    public interface IApplyDiscountStrategy
    {
        void ApplyDiscount(Order order);
    }
    

    public interface IDiscountStrategy
    {
        bool CanDiscount(Order order);
        void ApplyDiscount(Order order);
    }

    public class HappyHoursDiscountStrategy : IDiscountStrategy
    {
        private readonly TimeSpan beginTime;
        private readonly TimeSpan endTime;
        private readonly decimal percentage;

        public HappyHoursDiscountStrategy(TimeSpan beginTime, TimeSpan endTime, decimal percentage)
        {
            this.beginTime = beginTime;
            this.endTime = endTime;
            this.percentage = percentage;
        }

        public bool CanDiscount(Order order)
        {
            return order.OrderDate.TimeOfDay >= beginTime
              && order.OrderDate.TimeOfDay <= endTime;
        }

        public void ApplyDiscount(Order order)
        {
            order.DiscountAmount = order.TotalAmount * percentage;
        }

    }

    public class CustomerDiscountStrategy : IDiscountStrategy
    {
        private readonly string lastChar;
        private readonly decimal discountAmount;

        public CustomerDiscountStrategy(string lastChar, decimal discountAmount)
        {
            this.lastChar = lastChar;
            this.discountAmount = discountAmount;
        }

        public void ApplyDiscount(Order order)
        {
            if (order.TotalAmount <= discountAmount)
            {
                order.DiscountAmount = order.TotalAmount - 0.99m;
            }
            else
            {
                order.DiscountAmount = discountAmount;
            }
        }

        public bool CanDiscount(Order order)
        {
            return order.Customer.FirstName.EndsWith(lastChar);
        }
    }

    public class HappyHoursCanDiscountStrategy : ICanDiscountStrategy
    {
        private readonly TimeSpan beginTime;
        private readonly TimeSpan endTime;

        public HappyHoursCanDiscountStrategy(TimeSpan beginTime, TimeSpan endTime)
        {
            this.beginTime = beginTime;
            this.endTime = endTime;
        }

        public bool CanDiscount(Order order)
        {
            return order.OrderDate.TimeOfDay >= beginTime
             && order.OrderDate.TimeOfDay <= endTime;
        }
    }

    public class PercentageApplyDiscountStrategy : IApplyDiscountStrategy
    {
        private readonly decimal percentage;

        public PercentageApplyDiscountStrategy(decimal percentage)
        {
            this.percentage = percentage;
        }

        public void ApplyDiscount(Order order)
        {
            order.DiscountAmount = order.TotalAmount * percentage;
        }
    }

    public class FixedApplyDiscountStategy : IApplyDiscountStrategy
    {
        private readonly decimal discountAmount;

        public FixedApplyDiscountStategy(decimal discountAmount)
        {
            this.discountAmount = discountAmount;
        }

        public void ApplyDiscount(Order order)
        {
            if (order.TotalAmount <= discountAmount)
            {
                order.DiscountAmount = order.TotalAmount - 0.99m;
            }
            else
            {
                order.DiscountAmount = discountAmount;
            }
        }
    }

    public class OrderCalculator
    {
        private readonly ICanDiscountStrategy canDiscountStrategy;
        private readonly IApplyDiscountStrategy applyDiscountStrategy;

        public OrderCalculator(
            ICanDiscountStrategy canDiscountStrategy, 
            IApplyDiscountStrategy applyDiscountStrategy)
        {
            this.canDiscountStrategy = canDiscountStrategy;
            this.applyDiscountStrategy = applyDiscountStrategy;
        }

        public void CalculateDiscount(Order order)
        {
            if (canDiscountStrategy.CanDiscount(order))
            {
                applyDiscountStrategy.ApplyDiscount(order);
            }
        }
    }

    public class OrderCalculatorObsolete
    {
        private readonly IDiscountStrategy discountStrategy;

        public OrderCalculatorObsolete(IDiscountStrategy discountStrategy)
        {
            this.discountStrategy = discountStrategy;
        }

        public void CalculateDiscount(Order order)
        {
            if (discountStrategy.CanDiscount(order))
            {
                discountStrategy.ApplyDiscount(order);
            }
        }
    }

    #region Model

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

    #endregion
}
