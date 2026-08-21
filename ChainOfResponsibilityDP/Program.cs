using System;

namespace MyApp
{

    public abstract class Handler       //Abstract Handler class
    {
        protected Handler? successor;

        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }
        public abstract void HandleRequest(int requestId);
    }

    public class Handler1 : Handler    //Concrete Handler class1
    {
        public override void HandleRequest(int requestId)
        {
            if(requestId>0 && requestId<10)
            {
                Console.WriteLine("This is a single digit number. Request Handled");
            }
            else if( successor!=null)
            {
                successor.HandleRequest(requestId);
            }
        }
    }

    public class Handler2 : Handler    //Concrete Handler class 2
    {
        public override void HandleRequest(int requestId)
        {
            if (requestId >= 10 && requestId < 100)
            {
                Console.WriteLine("This is a double digit number. Request Handled");
            }
            else if(successor != null)
            {
                successor.HandleRequest(requestId);
            }
        }
    }

    public class Handler3 : Handler    //Concrete Handler class 3
    {
        public override void HandleRequest(int requestId)
        {
            if (requestId >= 100 && requestId < 1000)
            {
                Console.WriteLine("This is a three digit number. Request Handled");
            }
            else if (successor != null)
            {
                successor.HandleRequest(requestId);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var singleDigitHandler = new Handler1();
            var doubleDigitHandler = new Handler2();
            var tripleDigitHandler = new Handler3();

            singleDigitHandler.SetSuccessor(doubleDigitHandler);
            doubleDigitHandler.SetSuccessor(tripleDigitHandler);

            singleDigitHandler.HandleRequest(1);
            singleDigitHandler.HandleRequest(10);
            singleDigitHandler.HandleRequest(100);
        }
    }
}