using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Adapter
{
    class PrinterFactory
    {
        public static IPrinter Create(string model)
        {
            if (model.StartsWith("HP"))
            {
                return new HpPrinterAdapter();
            }
            else
            if (model.StartsWith("Canon"))
            {
                return new CanonPrinterAdapter();
            }

            throw new NotSupportedException();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            // Problem();
            IPrinter printer = PrinterFactory.Create("HP 1018");
            printer.Print("plik.txt", 10);
        }



        private static void Problem()
        {
            bool hasHp = true;

            if (hasHp)
            {
                HpPrinter printer = new HpPrinter();
                printer.Init();
                printer.Print("plik.txt");
                printer.Print("plik2.txt");
                printer.Release();
            }
            else
            {
                CanonPrinter printer = new CanonPrinter();
                printer.PrintFile("plik.txt");
            }
        }
    }

    public class CanonPrinter
    {
        private int counter = 0;

        public void PrintFile(string filename)
        {
            if (counter<100)
            {
                Console.WriteLine($"Printing {filename}...");
            }
        }
    }


    public class HpPrinter
    {
        private bool isPowerOn;


        public void Init()
        {
            isPowerOn = true;
        }

        public void Print(string filename, int copies = 1)
        {
            if (isPowerOn)
            {
                Console.WriteLine($"Printing {filename}...");
            }
        }

        public void Release()
        {
            isPowerOn = false;
        }
    }
}
