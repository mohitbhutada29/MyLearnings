//Factory method design pattern
using System;

namespace MyApp
{

    public abstract class Employee  //Abstract Product
    {
        public string? Name { get; set; }
        public int Id { get; set; }

        public int Designation { get; set; }

        protected Employee(string name, int id) 
        {
            Name = name;
            Id = id;
        }

        public abstract void PrintDetails();
      
    }

    public class Intern : Employee //Concrete Product
    {
        public Intern(string name, int id) : base(name, id){}

        public override void PrintDetails()
        {
            Console.WriteLine($"Intern Name={Name}");
        }
    }

    public class Manager : Employee //Concrete Product
    {
        public Manager(string name, int id) : base(name, id) { }
        public override void PrintDetails()
        {
            Console.WriteLine($"Manager Name={Name}");
        }
    }

    public class SoftwareEngineer : Employee //Concrete Product
    {
        public SoftwareEngineer(string name, int id) : base(name, id) { }
        public override void PrintDetails()
        {
            Console.WriteLine($"SoftwareEngineer Name={Name}");
        }
    }



    public abstract class EmployeeFactory() // Abstract Factory with Create method
    {
        public abstract Employee CreateEmployee(string name, int id);
    }

    public class InternFactory() : EmployeeFactory //Concrete factories
    {
        public override Employee CreateEmployee(string name, int id) => new Intern(name, id);
    }

    public class ManagerFactory() : EmployeeFactory  //Concrete factories
    {
        public override Employee CreateEmployee(string name, int id) => new Manager(name, id);
    }

    public class SoftwareEngineerFactory() : EmployeeFactory  //Concrete factories
    {
        public override Employee CreateEmployee(string name, int id)=>new SoftwareEngineer(name, id);
    }



    internal class Program  //Client
    {
        static void Main(string[] args)
        {

            EmployeeFactory employeeFactory =new InternFactory();
            var intern=employeeFactory.CreateEmployee("Intern1", 1);
            intern.PrintDetails();

            employeeFactory=new SoftwareEngineerFactory();
            var se=employeeFactory.CreateEmployee("SE2", 2);
            se.PrintDetails();

            employeeFactory=new ManagerFactory();
            var manager=employeeFactory.CreateEmployee("Manager3", 3);
            manager.PrintDetails();

        }
    }


}