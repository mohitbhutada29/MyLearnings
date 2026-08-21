using System;

namespace MyApp
{
    class TreeType //Flyweight class
    {
        public string Name { get; }
        public string Color { get; }
        public string Texture { get; }

        public TreeType(string name, string color, string texture)
        {
            Name = name;
            Color = color;
            Texture = texture;
        }

        public void Draw(int x, int y)
        {
            Console.WriteLine($"Drawn a {Name} tree with {Color} color and {Texture} texture at ({x}, {y})");
        }
    }

    class TreeFactory   //Flyweight Factory
    {
        private static readonly Dictionary<string, TreeType> _treeTypes = new();
        public static TreeType GetTreeType(string name, string color, string texture)
        {
            var key = $"{name}_{color}_{texture}";
            if( !_treeTypes.ContainsKey(key) )
            {
                var newType=new TreeType(name, color, texture);
                _treeTypes[key] = newType;
            }
            return _treeTypes[key];
        }
    }

    class Tree      //Context class
    {
        private readonly TreeType _treeType;
        private readonly int _x;
        private readonly int _y;

        public Tree(int x, int y, TreeType treeType)
        {
            _x = x;
            _y = y;
            _treeType = treeType;
        }

        public void Draw()
        {
            _treeType.Draw(_x, _y);
        }
    }
    internal class Program  //Client
    {
        static void Main(string[] args)
        {
            var forest = new List<Tree>();

            // Adding trees to forest
            forest.Add(new Tree(1, 2, TreeFactory.GetTreeType("Pine", "Green", "PineTexture")));
            forest.Add(new Tree(10, 20, TreeFactory.GetTreeType("Pine", "Green", "PineTexture")));
            forest.Add(new Tree(15, 25, TreeFactory.GetTreeType("Oak", "Brown", "OakTexture")));

            // Drawing trees
            foreach (var tree in forest)
            {
                tree.Draw();
            }
        }
    }
}