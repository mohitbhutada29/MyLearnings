using System;

namespace MyApp
{
    public interface IISubscriber   //Observer interface
    {
        string Name { get; set; }
        void Update();
    }

    public class Subscriber : IISubscriber    //Concrete Subscriber
    {
        public string Name { get ; set ; }

        public Subscriber(string name)
        {
            Name = name;
        }

        public void Update()
        {
            Console.WriteLine($"{Name} has been updated of the new song");
        }
    }

    public interface IMusicCompany  //Publisher interface
    {
        void AddSubscriber(IISubscriber subscriber);

        void RemoveSubscriber(IISubscriber subscriber);

        void NotifySubscribers();
    }

    public class TSeries : IMusicCompany //Concrete Publisher
    {
        public List<IISubscriber> Subscribers { get; set; } = new();    // Publisher has a list of all the subscribers

        public void AddSubscriber(IISubscriber subscriber)
        {
            Subscribers.Add(subscriber);
        }

        public void NotifySubscribers()
        {
            foreach (IISubscriber subscriber in Subscribers)
            {
                subscriber.Update();
            }
        }

        public void RemoveSubscriber(IISubscriber subscriber)
        {
            int index = Subscribers.FindIndex(x => (x.Name == subscriber.Name));
            Subscribers.RemoveAt(index);
        }
    }

    internal class Program      //Client
    {
        private static void Main(string[] args)
        {
            IMusicCompany company = new TSeries();
            company.AddSubscriber(new Subscriber ("Mohit"));
            company.AddSubscriber(new Subscriber ("Ankita"));
            company.AddSubscriber(new Subscriber ("Kanchan"));
            company.AddSubscriber(new Subscriber ("Mayur"));
        

            company.NotifySubscribers();

            company.RemoveSubscriber(new Subscriber("Mayur"));

            company.NotifySubscribers();
        }
    }
}