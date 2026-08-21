using System;

namespace MyApp
{
    public interface IFileSystem   
    {
        void Display(int depth);
    }

    public class File : IFileSystem     //Leaf
    {
        private string _name;

        public File(string name)
        {
            _name = name;
        }
        void IFileSystem.Display(int depth)
        {
            Console.WriteLine($"-{depth} {_name}");
        }
    }

    public class Folder : IFileSystem   //Composite
    {
        private string _name;
        private List<IFileSystem>? _items=new List<IFileSystem>();

        public Folder(string name)
        {
            _name=name;
        }

        public void Add(IFileSystem item)
        {
            _items?.Add(item); 
        }

        public void Remove(IFileSystem item)
        {
            if(_items!=null && _items.Contains(item))
            {
                _items.Remove(item);
            }
        }

        public void Display(int depth)
        {
            Console.WriteLine($"-{depth} {_name}");
            foreach (var item in _items)
            {
                item.Display(depth + 2);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var file1 = new File("File1");
            var file2 = new File("File2");

            var folder1 = new Folder("Folder1");
            folder1.Add(file1);

            var folder2 = new Folder("Folder2");
            folder2.Add(file2);
            folder2.Add(folder1);


            folder2.Display(1);
        }
    }
}