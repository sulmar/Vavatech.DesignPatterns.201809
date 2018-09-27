using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer("Marcin", "Sulecki");

            // string firstName = customer.FirstName;

            customer.BeginEdit();

            customer.FirstName = "Bartek";

            customer.CancelEdit();

            // customer.FirstName = firstName;

        }
    }


    public class Customer : IEditableObject, ICloneable
    {
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        private Customer last;

        public void BeginEdit()
        {
            last = (Customer) this.Clone();
        }

        public void CancelEdit()
        {
            this.FirstName = last.FirstName;
            this.LastName = last.LastName;
        }

        public void EndEdit()
        {
            last = null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
