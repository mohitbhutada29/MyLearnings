using System;

namespace MyApp
{
    public interface ICommand     //The request is converted into object of type ICommand
    {
        public void Execute();
    }

    public class Television     //Receiver who performs the actual work
    {
        public void TurnOn()
        {
            Console.WriteLine("Turning on...");
        }

        public void TurnOff()
        {
            Console.WriteLine("Turning off...");
        }

        public void IncreaseVolume()
        {
            Console.WriteLine("Increasing Volume...");
        }

        public void DecreaseVolume()
        {
            Console.WriteLine("Decreasing Volume...");
        }
    }

    public class TurnOnCommand : ICommand      // Concrete Command 1
    {
        private Television _tv;

        public TurnOnCommand(Television television)
        {
            _tv = television;
        }

        void ICommand.Execute()
        {
            _tv.TurnOn();
        }
    }

    public class TurnOffCommand : ICommand    // Concrete Command 2
    {
        private Television _tv;

        public TurnOffCommand(Television television)
        {
            _tv= television;
        }

        void ICommand.Execute()
        {
            _tv.TurnOff();
        }
    }

    public class IncreaseVolumeCommand : ICommand    // Concrete Command 3
    {
        private Television _tv;

        public IncreaseVolumeCommand(Television television)
        {
            _tv = television;
        }

        void ICommand.Execute()
        {
            _tv.IncreaseVolume();
        }
    }

    public class DecreaseVolumeCommand : ICommand    // Concrete Command 4
    {
        private Television _tv;

        public DecreaseVolumeCommand(Television television)
        {
            _tv = television;
        }

        void ICommand.Execute()
        {
            _tv.DecreaseVolume();
        }
    }

    public class RemoteControl  // Invoker class
    {
        private ICommand _command;
        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void PressButton()   //Invoker Method
        {
            _command.Execute();
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            var television=new Television();    
            var turnOnCommand=new TurnOnCommand(television);
            var turnOffCommand=new TurnOffCommand(television);  
            var volUpCommand=new IncreaseVolumeCommand(television);  
            var volDownCommand=new DecreaseVolumeCommand(television);  

            var remoteControl=new RemoteControl();
            remoteControl.SetCommand(turnOnCommand);
            remoteControl.PressButton();

            remoteControl.SetCommand(turnOffCommand);
            remoteControl.PressButton();

            remoteControl.SetCommand(volUpCommand);
            remoteControl.PressButton();

            remoteControl.SetCommand(volDownCommand);
            remoteControl.PressButton();
        }
    }
}