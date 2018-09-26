using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.MethodFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz typ klienta: (s)tandard (v)ip");

            string input = Console.ReadLine();

            Customer customer = CustomerFactory.Create(input);

            #region 
            //if (input == "s")
            //{
            //    customer = new StandardCustomer();
            //}
            //else if (input == "v")
            //{
            //    customer = new VipCustomer();
            //}
            //else if (input == "x")
            //{
            //    customer = new ExtraCustomer();
            //}
            //else
            //{
            //    throw new NotSupportedException();
            //}

            #endregion

            customer.Calculate();

        }
    }

    public abstract class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual void Calculate()
        {
            Console.WriteLine("calculating...");
        }

    }

    public class ExtraCustomer : Customer
    {
        public override void Calculate()
        {
            Console.WriteLine("eXtra calculating...");
        }
    }

    public class StandardCustomer : Customer
    {
    }

    public class VipCustomer : Customer
    {
        public decimal Discount { get; set; }

        public override void Calculate()
        {
            Console.WriteLine("Discount calculating...");
        }

    }



}
