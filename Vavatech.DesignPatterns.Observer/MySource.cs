using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Observer
{
    public class MySource : IObservable<float>, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("bye bye");
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            observer.OnNext(100f);
            observer.OnNext(56.6f);
            observer.OnNext(3.67f);

            observer.OnError(new Exception("Błąd pomiaru"));
            observer.OnNext(1.4f);
            observer.OnNext(10.4f);

            observer.OnCompleted();

            return this;


        }
    }


    public class MyObserver : IObserver<float>
    {
        public MyObserver(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void OnCompleted()
        {
            Console.WriteLine($"[{Name}] Koniec nadawania");
        }

        public void OnError(Exception error)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{Name}] -> {error.Message}");
            Console.ResetColor();
        }

        public void OnNext(float value)
        {
            Console.WriteLine($"[{Name}] -> temp: {value}");
        }
    }
}
