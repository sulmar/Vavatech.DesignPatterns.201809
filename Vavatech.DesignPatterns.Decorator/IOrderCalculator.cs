using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.DesignPatterns.Decorator.Models;

namespace Vavatech.DesignPatterns.Decorator
{
    public interface IOrderCalculator
    {
        void CalculateDiscount(Order order);
    }

    public class NoDiscountCalculator : IOrderCalculator
    {
        public NoDiscountCalculator()
        {
        }

        public void CalculateDiscount(Order order)
        {
        }
    }

    public class CalculatorDecorator : IOrderCalculator
    {
        private IOrderCalculator orderCalculator;

        private ICanDiscountStrategy canDiscountStrategy;
        private IApplyDiscountStrategy applyDiscountStrategy;

        public CalculatorDecorator(
            IOrderCalculator orderCalculator,
            ICanDiscountStrategy canDiscountStrategy,
            IApplyDiscountStrategy applyDiscountStrategy
           ) 
        {
            this.orderCalculator = orderCalculator;
            this.canDiscountStrategy = canDiscountStrategy;
            this.applyDiscountStrategy = applyDiscountStrategy;
        }

        public void CalculateDiscount(Order order)
        {
            this.orderCalculator.CalculateDiscount(order);

            if (canDiscountStrategy.CanDiscount(order))
            {
                decimal discountAmount = order.DiscountAmount;

                applyDiscountStrategy.ApplyDiscount(order);

                order.DiscountAmount += discountAmount;
            }
        }
    }

    //public class HappyHoursCalculatorDecorator : CalculatorDecorator
    //{
    //    private ICanDiscountStrategy canDiscountStrategy;
    //    private IApplyDiscountStrategy applyDiscountStrategy;

    //    public HappyHoursCalculatorDecorator(
    //        IOrderCalculator orderCalculator,  
    //        ICanDiscountStrategy canDiscountStrategy,
    //        IApplyDiscountStrategy applyDiscountStrategy
    //       ) : base(orderCalculator)
    //    {
    //        this.canDiscountStrategy = canDiscountStrategy;
    //        this.applyDiscountStrategy = applyDiscountStrategy;
    //    }

      
    //}
}
