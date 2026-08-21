using System;

namespace MyApp
{

    public interface ICoffee   //interface or product
    {
        public double Cost();

        public string Description();
    }

    public class SimpleCoffee : ICoffee //Concrete product
    {
        public double Cost()
        {
            return 5.5;
        }

        public string Description()
        {
            return "Simple Coffee";
        }
    }

    public abstract class CoffeeDecorator : ICoffee     //Decorator abstract class
    {

        protected ICoffee _coffee;
        public CoffeeDecorator(ICoffee coffee)
        {
            _coffee=coffee;
        }

        public virtual double Cost() 
        {
            return _coffee.Cost();  
        }

        public virtual string Description()
        {
            return _coffee.Description();
        }
    }

    public class MilkDecorator : CoffeeDecorator    //Concrete decorator
    {
        public MilkDecorator(ICoffee coffee) : base(coffee)
        {
        }

        public override double Cost()
        {
            return _coffee.Cost()+2;
        }

        public override string Description()
        {
            return _coffee.Description()+ " With Milk";
        }

    }

    public class SugarDecorator : CoffeeDecorator   //Concrete Decorator
    {
        public SugarDecorator(ICoffee coffee) : base(coffee)
        {
        }

        public override double Cost()
        {
            return _coffee.Cost() + 1;
        }

        public override string Description()
        {
            return _coffee.Description() + " With Sugar";
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            var coffee = new SimpleCoffee();
            Console.WriteLine($"{coffee.Description()}= {coffee.Cost()} rs");

            var milkCoffee=new MilkDecorator(coffee);
            Console.WriteLine($"{milkCoffee.Description()}= {milkCoffee.Cost()} rs");

            var sugarCoffee=new SugarDecorator(coffee);
            Console.WriteLine($"{sugarCoffee.Description()}= {sugarCoffee.Cost()} rs");
        }
    }
}