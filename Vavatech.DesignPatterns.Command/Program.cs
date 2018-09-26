using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Command
{
    class Program
    {
        static void Main(string[] args)
        {

            ICommand command = new PrintCommand(1, "plik.txt");

            command.Execute();

            command = 
                new SendCommand("marcin.sulecki@gmail.com", "marcin.sulecki@gmail.com", "Test");

            command.Execute();

        }
    }


    public interface ICommand
    {
        void Execute();
        bool CanExecute();
    }


    public class SendCommand : ICommand
    {
        private string from;
        private string to;
        private string message;

        public SendCommand(string from, string to, string message)
        {
            this.from = from;
            this.to = to;
            this.message = message;
        }

        public bool CanExecute()
        {
            return !string.IsNullOrEmpty(from)
                && !string.IsNullOrEmpty(to)
                && !string.IsNullOrEmpty(message);
        }

        public void Execute()
        {
            if (CanExecute())
            {
                Console.WriteLine($"{from} -> {to} printed {message}");
            }
        }
    }

    public class PrintCommand : ICommand
    {
        private int copies;
        private string filename;

        public PrintCommand(int copies, string filename)
        {
            this.copies = copies;
            this.filename = filename;
        }

        public bool CanExecute() => true;

        public void Execute()
        {
            for (int copy = 0; copy < copies; copy++)
            {
                Console.WriteLine($"{filename} copy#{copy}");
            }
        }
    }

    public class Printer
    {
        int quantity;

        public void Print(string filename, int copies)
        {
            for (int copy = 0; copy < copies; copy++)
            {
                Console.WriteLine($"{filename} copy#{copy}");
            }
        }

        public bool CanPrint()
        {
            return true;
        }

        public void Send(string from, string to, string message)
        {
            Console.WriteLine($"{from} -> {to} printed {message}");
        }

        public bool CanSend()
        {
            return quantity < 100;
        }
    }
}
