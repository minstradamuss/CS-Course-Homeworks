using HashTable;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        HashTable<string, int> hashTable = new HashTable<string, int>(10);

        hashTable.Add("one", 1);
        hashTable.Add("two", 2);
        hashTable.Add("three", 3);
        
        int valueForTwo = hashTable.Get("two");
        Debug.Assert(valueForTwo == 2, "Value for 'two' should be 2");

        hashTable.Remove("two");

        bool containsTwo = hashTable.ContainsKey("two");
        Debug.Assert(containsTwo == false, "HashTable should not contain 'two' after remove");
        Debug.Assert(hashTable.ContainsKey("one") == true, "HashTable should contain 'one'");
        Debug.Assert(hashTable.ContainsKey("three") == true, "HashTable should contain 'three'");

        int valueForOne = hashTable.Get("one");
        Debug.Assert(valueForOne == 1, "Value for 'one' should be 1");

        Console.WriteLine("OK!");
    }
}