using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Prototyp
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer(2, "Sulecki");

            Invoice invoice = new Invoice(1, "FA001/09/2018", customer, 100);

            Invoice copyInvoice = (Invoice) invoice.Clone();

            //Invoice copyInvoice = new Invoice(invoice.Id, "F002/09/2018", ;
            //copyInvoice.CreateDate = DateTime.Today;
            //copyInvoice.Number = "F002/09/2018";




        }
    }


    public class Invoice : ICloneable
    {

        protected Invoice()
        {

        }


        public Invoice(int id, string number, Customer customer, decimal totalAmount)
        {
            if (customer==null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (totalAmount<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalAmount));
            }

            Id = id;
            Number = number;
            Customer = customer;
            TotalAmount = totalAmount;
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime CreateDate { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool IsDelivered { get; set; }

        //private Invoice Copy()
        //{
        //    // płytka kopia
        //    return (Invoice) this.MemberwiseClone();

        //    //Invoice copyInvoice = new Invoice();
        //    //copyInvoice.CreateDate = this.CreateDate;
        //    //copyInvoice.Number = this.Number;
        //    //copyInvoice.DeliveryDate = this.DeliveryDate;
        //    //copyInvoice.Customer = this.Customer;
        //    //copyInvoice.TotalAmount = this.TotalAmount;

        //   // return copyInvoice;
        //}

        public object Clone() => this.MemberwiseClone();
    }

    public class Customer
    {
        public Customer(int id, string lastName)
        {
            Id = id;
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
