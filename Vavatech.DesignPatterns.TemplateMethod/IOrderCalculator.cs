using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.TemplateMethod
{
    public interface IOrderCalculator
    {
        void CalculateDiscount(Order order);
    }

    // Template method
    public abstract class BaseOrderCalculator
    {
        protected abstract bool CanDiscount(Order order);
        protected abstract void ApplyDiscount(Order order);

        public void CalculateDiscount(Order order)
        {
            if (CanDiscount(order))
            {
                ApplyDiscount(order);
            }
        }
    }

    public class HappyHoursOrderCalculator : BaseOrderCalculator
    {
        private readonly TimeSpan beginTime;
        private readonly TimeSpan endTime;

        private readonly decimal percentage;

        public HappyHoursOrderCalculator(TimeSpan beginTime, TimeSpan endTime, decimal percentage)
        {
            this.beginTime = beginTime;
            this.endTime = endTime;
            this.percentage = percentage;
        }

        protected override void ApplyDiscount(Order order)
        {
            order.DiscountAmount = order.TotalAmount * percentage;
        }

        protected override bool CanDiscount(Order order)
        {
            return order.OrderDate.TimeOfDay >= beginTime
                && order.OrderDate.TimeOfDay <= endTime;
        }
    }

    public class CustomerOrderCalculator : BaseOrderCalculator
    {
        private readonly string lastChar;
        private readonly decimal discountAmount;

        public CustomerOrderCalculator(string lastChar, decimal discountAmount)
        {
            this.lastChar = lastChar;
            this.discountAmount = discountAmount;
        }

        protected override bool CanDiscount(Order order)
        {
            return order.Customer.FirstName.EndsWith(lastChar);
        }

        protected override void ApplyDiscount(Order order)
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

    public class CustomerOrderCalculatorObsolete : IOrderCalculator
    {
        private readonly string lastChar;
        private readonly decimal discountAmount;

        public CustomerOrderCalculatorObsolete(string lastChar, decimal discountAmount)
        {
            this.lastChar = lastChar;
            this.discountAmount = discountAmount;
        }

        public void CalculateDiscount(Order order)
        {
            if (order.Customer.FirstName.EndsWith(lastChar))
            {
                if (order.TotalAmount<=discountAmount)
                {
                    order.DiscountAmount = order.TotalAmount - 0.99m;
                }
                else
                {
                    order.DiscountAmount = discountAmount;
                }
            }
        }
    }

    public class HappyHoursOrderCalculatorObsolete : IOrderCalculator
    {
        private readonly TimeSpan beginTime;
        private readonly TimeSpan endTime;

        private readonly decimal percentage;

        public HappyHoursOrderCalculatorObsolete(TimeSpan beginTime, TimeSpan endTime, decimal percentage)
        {
            this.beginTime = beginTime;
            this.endTime = endTime;
            this.percentage = percentage;
        }

        public void CalculateDiscount(Order order)
        {
            if (order.OrderDate.TimeOfDay >= beginTime 
                && order.OrderDate.TimeOfDay <= endTime)
            {
                order.DiscountAmount = order.TotalAmount * percentage;
            }

        }
    }
}
