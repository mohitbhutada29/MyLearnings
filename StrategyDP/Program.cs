using System;

namespace MyApp
{
    interface ICompressionStrategy  //Strategy interface
    {
        void Compress(string fileName);
    }

    class ZipCompression : ICompressionStrategy //Concrete strategy 1
    {
        public void Compress(string fileName)
        {
            Console.WriteLine($"Your file {fileName} has been compressed using Zip compression");
        }
    }

    class RarCompression : ICompressionStrategy //Concrete strategy 2
    {
        public void Compress(string fileName)
        {
            Console.WriteLine($"Your file {fileName} has been compressed using Rar compression");
        }
    }

    /// <summary>
    /// // The context class contains a property that holds the reference of a Concrete Strategy object.
    /// The client will set this property at run-time according to the required algorithm. 
    /// </summary>
    class Context 
    {
        private ICompressionStrategy _compressionStrategy;

        public Context(ICompressionStrategy compressionStrategy)
        {
            _compressionStrategy = compressionStrategy; 
        }

        public void ContextCompression(string fileName)
        {
            _compressionStrategy.Compress(fileName);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var context=new Context(new RarCompression());
            context.ContextCompression("RarCompressionFileName");

            context = new Context(new ZipCompression());
            context.ContextCompression("ZipCompressionFileName");

        }
    }
}