using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Decorator.Models
{
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

}
