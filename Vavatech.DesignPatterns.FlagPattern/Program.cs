using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.DesignPatterns.FlagPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Projector projector = new Projector("XYZ 001");

            projector.State = ProjectorState.LampOn | ProjectorState.CabelConnected;

            Console.WriteLine(projector.State);

            if (projector.State.HasFlag(ProjectorState.CabelConnected))
            {
                Console.WriteLine("Kabel podłączony");
            }

        }
    }


    public class Projector
    {
        public Projector(string model)
        {

            Model = model;
        }

        public string Model { get; set; }
        public ProjectorState State { get; set; }
    }

    [Flags]
    public enum ProjectorState
    {
        LampOn          = 1,
        SwitchOn        = 2,
        FlapOpened      = 4,
        CabelConnected  = 8

    }

}
