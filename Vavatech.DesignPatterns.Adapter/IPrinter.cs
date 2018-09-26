using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Adapter
{
    public abstract class PrinterParameters
    {
    }

    public class HpPrinterParameters : PrinterParameters
    {
        public int Copies { get; set; }
    }

    public class CanonPrinterParameters : PrinterParameters
    {
        public bool PrintColor { get; set; }
    }

    public interface IPrinter
    {
        void Print(string filename, int copies);
    }

    public class HpPrinterAdapter : IPrinter
    {
        private HpPrinter printer;

        public HpPrinterAdapter()
        {
            printer = new HpPrinter();
        }

        public void Print(string filename, int copies)
        {
            printer.Init();
            printer.Print(filename, copies);
            printer.Release();
        }
    }

    public class CanonPrinterAdapter : IPrinter
    {
        private CanonPrinter printer;

        public CanonPrinterAdapter()
        {
            printer = new CanonPrinter();
        }

        public void Print(string filename, int copies)
        {
            for (int i = 0; i < copies; i++)
            {
                printer.PrintFile(filename);
            }
        }
    }
}
