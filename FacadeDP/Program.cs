using System;
using System.Threading.Channels;

namespace MyApp
{
    public class Product
    {
        public void GetProductDetails()
        {
            Console.WriteLine("Getting Product Details...");
        }
    }

    public class Invoice
    {
        public void SendInvoice()
        {
            Console.WriteLine("Invoice sent succesfully");
        }
    }

    public class Payment
    {
        public void MakePayment()
        {
            Console.WriteLine("Payment done succesfully");
        }
    }

    public class Facade //Acts as a higher interface or class which hides all the complexities from the client
    {
        public void Order()
        {
            Console.WriteLine("Ordering the product\n");

            var product = new Product();
            product.GetProductDetails();

            var invoice=new Invoice();
            invoice.SendInvoice();

            var payment=new Payment();
            payment.MakePayment();

            Console.WriteLine("\nProduct ordered succesfully");
        }
    }


    internal class Program  // Client directly uses facade class. The complexities are hidden from the client
    {
        static void Main(string[] args)
        {
            var facade = new Facade();
            facade.Order();
        }
    }
}