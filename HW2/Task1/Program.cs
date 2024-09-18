using HT;

namespace Task1
{
    class MainClass
    {
        public static void Main()
        {
            HashTable table = new HashTable();

            Console.WriteLine($"Add 1: {table.Add(1)}");
            Console.WriteLine($"Add 2: {table.Add(2)}");
            Console.WriteLine($"Add 3: {table.Add(3)}");

            Console.WriteLine($"Contains 1: {table.Contains(1)}");
            Console.WriteLine($"Contains 2: {table.Contains(2)}");
            Console.WriteLine($"Contains 3: {table.Contains(3)}");
            Console.WriteLine($"Contains 4: {table.Contains(4)}");

            Console.WriteLine($"Remove 3: {table.Remove(3)}");
            Console.WriteLine($"Remove 4: {table.Remove(4)}");
            Console.WriteLine($"Remove 3 (again): {table.Remove(3)}");

            Console.WriteLine($"Contains 1: {table.Contains(1)}");
            Console.WriteLine($"Contains 2: {table.Contains(2)}");
            Console.WriteLine($"Contains 3: {table.Contains(3)}");
            Console.WriteLine($"Contains 4: {table.Contains(4)}");
        }
    }
}