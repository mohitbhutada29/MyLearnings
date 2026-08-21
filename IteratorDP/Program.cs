using System;
using System.Diagnostics;
using System.Transactions;

namespace MyApp
{
    internal class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    internal interface IIAggregate
    {
        IIIterator CreateIterator();
    }

    internal interface IIIterator
    {
        Employee First();

        Employee? Next();

        bool IsCompleted { get; }
    }

    internal class Aggregate : IIAggregate  //contains a list of items and create iterator instance
    {
        public List<Employee> ListOfEmployees { get; set; } = new();
        public int Count { get => ListOfEmployees.Count; }

        public IIIterator CreateIterator()
        {
            return new Iterator(this);
        }

        public void AddEmployee(Employee employee)
        {
            ListOfEmployees.Add(employee);
        }

        public Employee GetEmployee(int indexPosition)
        {
            return ListOfEmployees[indexPosition];
        }
    }

    internal class Iterator : IIIterator
    {
        private Aggregate aggregate;
        private int current = 0;
        private int step = 1;

        public Iterator(Aggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        public bool IsCompleted
        {
            get { return current >= aggregate.Count; }
        }

        public Employee First()
        {
            current = 0;
            return aggregate.ListOfEmployees[current];
        }

        public Employee? Next()
        {
            current += step;

            return IsCompleted ? null : aggregate.ListOfEmployees[current];
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var aggregate=new Aggregate();  
            aggregate.AddEmployee(new Employee { Id=1, Name="Mohit"});
            aggregate.AddEmployee(new Employee { Id=2, Name="ohit"});
            aggregate.AddEmployee(new Employee { Id=3, Name="hit"});
            aggregate.AddEmployee(new Employee { Id=4, Name="it"});
            aggregate.AddEmployee(new Employee { Id=5, Name="t"});


            var iterator=new Iterator(aggregate);

            for (Employee emp = iterator.First(); !iterator.IsCompleted; emp= iterator.Next())
            {
                Console.WriteLine($"Employee Name={emp.Name}");
            }
        }
    }
}