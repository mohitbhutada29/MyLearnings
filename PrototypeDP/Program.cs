using System;

namespace MyApp
{
    internal interface IPrototype<T> //Prototype Interface
    {
        T Clone();
    }

    public class Person : IPrototype<Person>  //Concrete Prototype
    {
        public string Name { get; set; }
        public double Age { get; set; }

        public Person(string name, double age)
        {
            Name = name;
            Age = age;
        }

        public Person Clone()
        {
            return new Person(Name, Age);
        }

        public string ToString()
        {
            return $"Name={Name} and Age={Age}";
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Person person = new Person("Mohit", 25);
            var clonedPerson = person.Clone();
            clonedPerson.Name = "Rohit";    //Original object remains the same
            clonedPerson.Age = 30;

            Console.WriteLine(person.ToString());
            Console.WriteLine(clonedPerson.ToString());
        }
    }
}