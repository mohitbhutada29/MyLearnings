using System;

namespace MyApp
{

  

    public interface  IIntern 
    {
        public string Name { get; set; }
    }

    public interface IManager
    {
        public string Name { get; set; }
    }

    public class L1Intern : IIntern
    {
        public string Name { get ; set ; }

        public L1Intern(string name) 
        {
            Name = name;
        }
    }

    public class L2Intern : IIntern
    {
        public string Name { get; set; }

        public L2Intern(string name)
        {
            Name = name;
        }
    }

    public class L1Manager : IManager
    {
        public string Name { get; set; }

        public L1Manager(string name)
        {
            Name = name;
        }
    }

    public class L2Manager : IManager
    {
        public string Name { get; set; }

        public L2Manager(string name)
        {
            Name = name;
        }
    }



    public interface IEmployeeFactory
    {
        public IIntern CreateIntern(string name);
        public IManager CreateManager(string name);
    }

    public class L1EmployeeFactory() : IEmployeeFactory
    {
        public IIntern CreateIntern(string name)
        {
            return new L1Intern(name);
        }

        public IManager CreateManager(string name)
        {
            return new L1Manager(name);
        }
    }

    public class L2EmployeeFactory() : IEmployeeFactory
    {
        public IIntern CreateIntern(string name)
        {
            return new L2Intern(name);
        }

        public IManager CreateManager(string name)
        {
            return new L2Manager(name);
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            IEmployeeFactory factory1 = new L1EmployeeFactory();    //Level 1 Factory
            IEmployeeFactory factory2 = new L2EmployeeFactory();     //Level 2 Factory

            var intern1 = factory1.CreateIntern("Intern1");
            var manager1 = factory1.CreateManager("Manager1");

            var intern2 = factory2.CreateIntern("Intern1");
            var manager2 = factory2.CreateManager("Manager1");
        }
    }
}