using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler customersService1 = new CacheCustomersService();
            Handler customersService2 = new DbCustomersService();
            customersService1.SetSuccessor(customersService2);

            // Generate and process request
            int[] requests = { 1, 2, 3, 5 };

            foreach (int request in requests)
            {
                Customer customer = customersService1.Get(request);

                Console.WriteLine(customer);
            }
        }
    }


    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"{Id} - {Name}";
    }

    public interface ICustomersService
    {
        Customer Get(int id);
    }

    // ConcreteHandler1
    public class CacheCustomersService : Handler, ICustomersService
    {
        private IList<Customer> customers = new List<Customer>()
    {
        new Customer { Id = 1, Name = "Company 1 from Cache" },
        new Customer { Id = 3, Name = "Company 3 from Cache " },
    };

        public override Customer Get(int id)
        {
            if (customers.Any(c => c.Id == id))
            {
                return customers.FirstOrDefault(c => c.Id == id);
            }
            else if (successor != null)
            {
                return successor.Get(id);
            }

            return null;
        }
    }

    // ConcreteHandler2
    public class DbCustomersService : Handler, ICustomersService
    {
        private IList<Customer> customers = new List<Customer>()
    {
        new Customer { Id = 1, Name = "Company 1 from Db" },
        new Customer { Id = 2, Name = "Company 2 from Db" },
        new Customer { Id = 3, Name = "Company 3 from Db" },
    };

        // HandleRequest
        public override Customer Get(int id)
        {
            Customer customer = null;

            if (customers.Any(c => c.Id == id))
            {
                customer = customers.FirstOrDefault(c => c.Id == id);
            }
            else if (successor != null)
            {
                customer = successor.Get(id);
            }

            return customer;
        }
    }


    public abstract class Handler
    {
        protected Handler successor;

        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }

        public abstract Customer Get(int id);
    }
}
