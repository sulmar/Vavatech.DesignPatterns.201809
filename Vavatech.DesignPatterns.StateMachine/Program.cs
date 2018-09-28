using Stateless.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.StateMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Lamp lamp = new Lamp();

            Console.WriteLine(lamp.State);

            while (true)
            {
                lamp.Down();

                lamp.Down();


                Console.WriteLine(lamp.State);

                lamp.Up();

                Console.WriteLine(lamp.State);
            }
        }
    }

    
    public class LampObsolete
    {
        public LampState State { get; set; }

        public LampObsolete()
        {
            State = LampState.Off;
        }

        public void Up()
        {
            if (State == LampState.On)
                State = LampState.Off;
        }

        public void Down()
        {
            if (State == LampState.Off)
                State = LampState.On;
            
        }
    }

    public class Lamp
    {
        private Stateless.StateMachine<LampState, LampTrigger> _machine;

        public Lamp()
        {
            _machine = 
                new Stateless.StateMachine<LampState, LampTrigger>(LampState.Off);

            _machine.Configure(LampState.Off)
                .OnEntry(() => Console.WriteLine("Dziękuję za wyłączenie światła."), "Send SMS")
                .Permit(LampTrigger.Down, LampState.On)
             //   .PermitReentry(LampTrigger.Up)
                ;

            _machine.Configure(LampState.On)
                .OnEntry(() => Console.WriteLine("Pamiętaj o wyłączeniu światła!"), "Send SMS")
                .Permit(LampTrigger.Up, LampState.Off)
                .PermitReentry(LampTrigger.Down)
                ;

            string graph = UmlDotGraph.Format(_machine.GetInfo());

            Console.WriteLine(graph);
        }

        public LampState State => _machine.State;

        public bool CanUp => _machine.CanFire(LampTrigger.Up);
        public bool CanDown => _machine.CanFire(LampTrigger.Down);

        public void Up()
        {
            _machine.Fire(LampTrigger.Up);
        }

        public void Down()
        {
            _machine.Fire(LampTrigger.Down);
        }
    }

    public enum LampState
    {
        Off,
        On
    }

    public enum LampTrigger
    {
        Up,
        Down
    }
    
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        New,
        Completion,
        Boxing,
        Canceled,
        Sent,
        Delivered,
        Done
    }
}
