using System;

namespace MyApp
{
    public abstract class Employee //superclass
    {
        protected string Name { get; }
        protected int Id { get; }

        public Employee(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public abstract void GiveEmployeeDetails();
    }

    public class Intern : Employee //derived class 1
    {
        public Intern(string name, int id) : base(name, id)
        {
        }

        public override void GiveEmployeeDetails()
        {
            Console.WriteLine($"InternName={Name}\nInternId={Id}");
        }
    }

    public class SoftwareEngineer : Employee //derived class 2
    {
        public SoftwareEngineer(string name, int id) : base(name, id)
        {
        }

        public override void GiveEmployeeDetails()
        {
            Console.WriteLine($"SoftwareEngineer={Name}\nSoftwareEngineerId={Id}");
        }
    }

    public class Manager : Employee //derived class 3
    {
        public Manager(string name, int id) : base(name, id)
        {
        }

        public override void GiveEmployeeDetails()
        {
            Console.WriteLine($"ManagerName={Name}\nManagerId={Id}");
        }
    }

    public class EmployeeFactory //factory
    {
        public Employee CreateEmployee(string employeeType,string name, int id)
        {
            switch (employeeType)
            {
                case "Intern":
                    return new Intern(name,id);

                case "SoftwareEngineer":
                    return new SoftwareEngineer(name, id);

                case "Manager":
                    return new Manager(name, id);

                default:
                    return null;
            }
        }
    }

    internal class Program  //Client
    {
        private static void Main(string[] args)
        {
            var employeeFactory=new EmployeeFactory();
            var softwareEngineer = employeeFactory.CreateEmployee("SoftwareEngineer", "Mohit", 1);
            softwareEngineer.GiveEmployeeDetails();

            var employee2 = employeeFactory.CreateEmployee("Intern", "Sukesh", 2);
            employee2.GiveEmployeeDetails();
            }
    }
}