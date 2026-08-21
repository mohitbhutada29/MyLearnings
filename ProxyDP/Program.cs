using System;

namespace MyApp
{
    public interface IBankProxy
    {
        void WithDrawMoney();
    }

    public class Bank : IBankProxy
    {
        public void WithDrawMoney()
        {
            Console.WriteLine("Withdrawing money from the account...");
        }
    }

    public class ATMProxy : IBankProxy
    {
        private Bank _bank;
        private string _customerType;

        public ATMProxy(string customerType)
        {
            _customerType=customerType;
        }
        public void WithDrawMoney()
        {
            if(_customerType == "ValidCustomer")
            {
                if (_bank == null)
                {
                    _bank = new Bank();
                }
                _bank.WithDrawMoney();
            }
            else
            {
                Console.WriteLine("Invalid customer, Returning from Proxy...");
            }
            
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var string1 = "こんにちは";
            var string2 = "こんにちは";

            Console.WriteLine(string1==string2);
        //    var atmObj = new ATMProxy("ValidCustomer");
        //    atmObj.WithDrawMoney();

        //    atmObj = new ATMProxy("RandomCustomer");
        //    atmObj.WithDrawMoney();
        }
    }
}