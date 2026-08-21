using System;

namespace MyApp
{
    public class Employee
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public double Salary { get; set; }
    }

    public interface IAdapter   //Adapter interface Or ITarget
    {
        void ProcessSalary(string[,] arrayOfEmployees);
    }

    public class EmployeeAdapter : IAdapter //Concrete Adapter
    {
        private ThirdPartyBillingSystem _billingSystem;

        public EmployeeAdapter()
        {
            _billingSystem = new ThirdPartyBillingSystem();
        }

        public void ProcessSalary(string[,] arrayOfEmployees)
        {
            var employees = new List<Employee>();
            for (int i = 0; i < arrayOfEmployees.GetLength(0); i++)
            {
                var newEmployee = new Employee();
                newEmployee.Id = int.Parse(arrayOfEmployees[i, 0]);
                newEmployee.Name = arrayOfEmployees[i, 1];
                newEmployee.Salary = double.Parse(arrayOfEmployees[i, 2]);
                employees.Add(newEmployee);
            }

            _billingSystem.ProcessSalaryForTheListOfEmployees(employees);
        }
    }

    public class ThirdPartyBillingSystem()      //Adaptee whose functionality needs to be used by the client
    {
        public void ProcessSalaryForTheListOfEmployees(List<Employee> listOfEmployees)
        {
            foreach (Employee emp in listOfEmployees)
            {
                Console.WriteLine($"Name={emp.Name}, Salary={emp.Salary}, Id={emp.Id}\n");
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args) //Client has access to Adapter but not directly to Adaptee
        {
            string[,] employeesArray = new string[5, 3]
           {
                {"101","John","10000"},
                {"102","Smith","20000"},
                {"103","Dev","30000"},
                {"104","Pam","40000"},
                {"105","Sara","50000"}
           };

            IAdapter adapter = new EmployeeAdapter();
            adapter.ProcessSalary(employeesArray);
        }
    }
}