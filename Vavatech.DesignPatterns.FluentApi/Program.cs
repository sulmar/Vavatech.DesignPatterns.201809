using CSharpVerbalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.FluentApi
{
    class Program
    {
        static void Main(string[] args)
        {
            StandardTest();

            FluentApiTest();

            VerbalExpressionsTest();


        }

        // PM> Install-Package VerbalExpressions-official
        private static void VerbalExpressionsTest()
        {
            // Create an example of how to test for correctly formed URLs
            var verbEx = new VerbalExpressions()
                        .StartOfLine()
                        .Then("http")
                        .Maybe("s")
                        .Then("://")
                        .Maybe("www.")
                        .AnythingBut(" ")
                        .EndOfLine();

            // Create an example URL
            var testMe = "https://www.google.com";

            Console.WriteLine(verbEx);

            if (verbEx.Test(testMe))
            {
                Console.WriteLine("The URL is incorrect");
            }

            Console.WriteLine("We have a correct URL ");
        }

        private static void StandardTest()
        {
            PhoneObsolete phone = new PhoneObsolete();
            phone.Model = "Nokia";

            phone.Call("555-066-555", "555-222-555");
        }

        private static void FluentApiTest()
        {
            Phone.Instance
                .From("555-066-555")
                .To("555-222-555")
                .WithSubject("W sprawie szkolenia")
                .Call();


            Phone.Instance
                .From("555-066-555")
                .To("555-222-555")
                .Call();
        }
    }

    public interface IFrom
    {
        ITo From(string from);
    }

    public interface ITo
    {
        ISubject To(string to);
    }

    public interface ISubject : ICall
    {
        ICall WithSubject(string subject);
    }

    public interface ICall
    {
        void Call();
    }

    public class Phone : IFrom, ITo, ISubject, ICall
    {
        private string _from;
        private string _to;
        private string _subject;

        public static IFrom Instance => new Phone();

        public ITo From(string from)
        {
            this._from = from;
            return this;
        }

        public ISubject To(string to)
        {
            this._to = to;

            return this;
        }

        public ICall WithSubject(string subject)
        {
            this._subject = subject;

            return this;
        }

        public void Call()
        {
            if (string.IsNullOrEmpty(_subject))
            {
                Console.WriteLine($"Calling {_from} {_to}");
            }
            else
            {
                Console.WriteLine($"Calling {_from} {_to} with {_subject}");
            }
            
        }


        
    }

    public class PhoneObsolete
    {
        public string Model { get; set; }

        public void Call(string from, string to)
        {
            Console.WriteLine($"Calling {from} {to}");
        }


    }
}
