using HashTable;
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        HashTable<int> hashTable = new HashTable<int>();

        Console.WriteLine("Добавляем элементы 1, 2, 3 в хэш-таблицу.");
        hashTable.Add(1);
        hashTable.Add(2);
        hashTable.Add(3);

        Console.WriteLine("Получаем элемент с значением 2:");
        int valueForTwo = hashTable.Get(2);
        Console.WriteLine($"Значение для 2: {valueForTwo}");

        Console.WriteLine("Удаляем элемент с значением 2.");
        hashTable.Remove(2);

        Console.WriteLine("Проверяем, содержится ли значение 2:");
        bool containsTwo = hashTable.Contains(2);
        Console.WriteLine($"Содержит ли таблица значение 2? {containsTwo}");

        Console.WriteLine("Проверяем, содержится ли значение 1:");
        bool containsOne = hashTable.Contains(1);
        Console.WriteLine($"Содержит ли таблица значение 1? {containsOne}");

        Console.WriteLine("Получаем элемент с значением 1:");
        int valueForOne = hashTable.Get(1);
        Console.WriteLine($"Значение для 1: {valueForOne}");
    }
}
