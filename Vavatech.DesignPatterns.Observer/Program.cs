using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            // PM> Install-Package System.Reactive

            // hot source
           // var subject = new System.Reactive.Subjects.Subject<int>();

            // cold source
           // var subject = new System.Reactive.Subjects.ReplaySubject<int>();

            Random random = new Random();

            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(p => random.Next(0, 100));

            source
                .Where(temp => temp > 70)
                .Subscribe(temp => Console.WriteLine($"#1 {temp}"));
            // source.Subscribe(temp => Console.WriteLine($"#2 {temp}"));


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            //subject.OnNext(100);
            //subject.OnNext(200);
            // subject.OnNext(300);


            //while (true)
            //{
            //    subject.OnNext(random.Next(0, 100));

            //    Thread.Sleep(TimeSpan.FromSeconds(1));
            //};

            // ObservableAndObserver();

            // ObserverStartup();

        }

        private static void ObservableAndObserver()
        {
            MySource source = new MySource();

            MyObserver observer = new MyObserver("Marcin");
            source.Subscribe(observer);


            MyObserver observer2 = new MyObserver("Bartek");
            source.Subscribe(observer2);
        }

        private static void ObserverStartup()
        {
            IObserver observer1 = new ConsoleObserver("Marcin");
            IObserver observer2 = new ConsoleObserver("Bartek");

            Sensor sensor = new Sensor(101);

            sensor.Subscribe(observer1);
            sensor.Subscribe(observer2);

            Lamp lamp = new Lamp();
            lamp.Subscribe(observer1);


            Random random = new Random();

            while (true)
            {
                sensor.Temperature = random.Next(0, 100);

                Thread.Sleep(TimeSpan.FromSeconds(1));

                lamp.SwitchOn = !lamp.SwitchOn;
            }
        }
    }


    public class Lamp : Subject
    {
        private bool switchOn;

        public bool SwitchOn
        {
            get { return switchOn; }
            set
            {
                switchOn = value;
                Notify();
            }
        }

        public override string ToString() => $"{SwitchOn}";

    }

    public class Sensor : Subject
    {
        public byte Id { get; set; }
        private float temperature;

        public Sensor(byte id)
        {
            Id = id;
        }

        public float Temperature
        {
            get { return temperature; }
            set
            {
                temperature = value;

                if (value > 70)
                {
                    Notify();
                }
            }
        }


        public override string ToString() => $"({Id}) Temperature: {Temperature}";
    }

    public abstract class Subject
    {
        private IList<IObserver> observers = new List<IObserver>();

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        protected void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Notify(this);
            }
        }
    }

    public interface IObserver
    {
        void Notify(Subject subject);
    }

    public class ConsoleObserver : IObserver
    {
        public ConsoleObserver(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void Notify(Subject subject)
        {
            Console.WriteLine($"[{Name}] {subject}");
        }
    }
}
