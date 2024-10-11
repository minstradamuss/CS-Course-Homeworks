public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}";
    }
}

public class NameLengthComparer : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        if (x == null || y == null) throw new ArgumentException("Null argument");
        int lengthComparison = x.Name.Length.CompareTo(y.Name.Length);
        if (lengthComparison != 0)
        {
            return lengthComparison;
        }
        return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
    }
}

public class AgeComparer : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        if (x == null || y == null) throw new ArgumentException("Null argument");

        return x.Age.CompareTo(y.Age);
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Person> people = new List<Person>
        {
            new Person("Alice", 30),
            new Person("Bob", 25),
            new Person("Charlie", 22),
            new Person("David", 35),
            new Person("Vasiliy", 40),
            new Person("Maria", 20)
        };

        Console.WriteLine("Сортировка по длине имени и потом по первой букве (при одинаковой длине):");
        people.Sort(new NameLengthComparer());
        foreach (var person in people)
        {
            Console.WriteLine(person);
        }

        Console.WriteLine("\nСортировка по возрасту:");
        people.Sort(new AgeComparer());
        foreach (var person in people)
        {
            Console.WriteLine(person);
        }
    }
}
