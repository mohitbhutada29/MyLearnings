using System;

namespace MyApp
{
    public class Person
    {
        public string? Name{ get; set; }

        public string? LastName { get; set; }

        public int Age { get; set; }

       

        public Person SetName(string name) //returning same context object Person
        {
            Name=name;
            return this;
        }

        public Person SetLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }

        public Person SetAge(int age)
        {
            Age = age;
            return this;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var person=new Person();
            person
                .SetName("Mohit")
                .SetLastName("Bhutada")
                .SetAge(25);                    //Method chaining

            Console.WriteLine($"Name={person.Name}, LastName={person.LastName}, Age={person.Age}");
        }
    }
}