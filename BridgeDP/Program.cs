using System;

namespace MyApp
{
    public interface IDevice //Implementor interface
    {
        void TurnOn();
        void TurnOff();
    }

    public class TV : IDevice   // Concrete Implementor 1
    {
        void IDevice.TurnOff()
        {
            Console.WriteLine("Turning off the TV");
        }

        void IDevice.TurnOn()
        {
            Console.WriteLine("Turning on the TV");
        }
    }

    public class Radio : IDevice    //Concrete Implementor 2
    {
        void IDevice.TurnOff()
        {
            Console.WriteLine("Turning off the Radio");
        }

        void IDevice.TurnOn()
        {
            Console.WriteLine("Turning on the Radio");
        }
    }

    public class RemoteControl //Abstraction
    {
        private IDevice _device;

        public RemoteControl(IDevice device)
        {
            _device = device;    
        }

        public void TurnOn()
        {
            _device.TurnOn();
        }

        public void TurnOff()
        {
            _device.TurnOff();
        }
    }
    internal class Program  //Client
    {
        static void Main(string[] args)     // The two layers: Abstraction and the Implementation can be modified without affecting each other
        {
            var tv=new TV();    
            var radio=new Radio();

            var remoteControl=new RemoteControl(tv);
            remoteControl.TurnOn();
            remoteControl.TurnOff();

            remoteControl=new RemoteControl(radio);
            remoteControl.TurnOn();
            remoteControl.TurnOff();
        }
    }
}