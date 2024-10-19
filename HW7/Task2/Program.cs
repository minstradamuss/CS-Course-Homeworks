class Program
{
    static void Main()
    {
        List<string> names = new List<string>
        {
            "Алексей",
            "Маша",
            "Дмитрий",
            "Катя",
            "Саша",
            "Роман",
            "Светлана"
        };

        Console.WriteLine("Выборка");
        foreach (var name in names.Select((name, index) => new { name, index }))
        {
            Console.WriteLine($"{name.index} {name.name} {name.name.Length}");
        }

        var result = names
            .Select((name, index) => new { Name = name, Index = index })
            .Where(x => x.Name.Length > x.Index)
            .Select(x => x.Name);

        Console.WriteLine("\nИмена с длиной > индекса:");
        foreach (var name in result)
        {
            Console.WriteLine(name);
        }
    }
}
