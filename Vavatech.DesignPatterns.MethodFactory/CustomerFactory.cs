using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.MethodFactory
{
    public class CustomerFactory
    {
        public static Customer Create(string input)
        {
            Customer customer;

            if (input == "s")
            {
                customer = new StandardCustomer();
            }
            else if (input == "v")
            {
                customer = new VipCustomer();
            }
            else if (input == "x")
            {
                customer = new ExtraCustomer();
            }
            else
            {
                throw new NotSupportedException();
            }

            return customer;
        }
    }
}
