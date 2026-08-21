namespace MyApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var myInstance1=MySingletonClass.GetInstance();
            var myInstance2=MySingletonClass.GetInstance();

        }
    }

    public sealed class MySingletonClass    // declare class as sealed to prevent inheritance
    {
        private static MySingletonClass? instance; // private static variable to hold the single instance of the class

        private MySingletonClass()  //private and parameter less constructor
        {
        }

        public static MySingletonClass GetInstance()    // public static method to provide access to the single instance of the class
        {
            if (instance == null)

            {
                instance = new MySingletonClass();
                Console.WriteLine("New instance created");
            }
            else
            {
                Console.WriteLine("Instance already created");
            }
            return instance;
        }


    }
}