//Write a program to create a delegate called TrafficDel and a class called TrafficSignal with the following delegate methods.
//Public static void Yellow()
//{
//    Console.WriteLine(“Yellow Light Signal To Get Ready”);
//}
//Public static void Green()
//{
//    Console.WriteLine(“Green Light Signal To Go”);
//}
//Public static void Red()
//{
//    Console.WriteLine(“Red Light Signal To Stop”);
//}

using System;
namespace TrafficDelegateExample
{

    class Program
    {
        static void Main(string[] args)
        {
            TrafficSignal ts = new TrafficSignal();
            ts.IdentifySignal();
            ts.display();
        }
    }
    public delegate void TrafficDel();
    class TrafficSignal
    {
        public static void Yellow()
        {
            Console.WriteLine("Yellow light signals to get ready");
        }
        public static void Green()
        {
            Console.WriteLine("Green light signals to go");
        }
        public static void Red()
        {
            Console.WriteLine("Red light signals to stop");
        }
        TrafficDel[] td = new TrafficDel[3];
        public void IdentifySignal()
        {
            td[0] = new TrafficDel(Yellow);
            td[1] = new TrafficDel(Green);
            td[2] = new TrafficDel(Red);
        }
        public void display()
        {
            td[0]();
            td[1]();
            td[2]();
        }
    }

}