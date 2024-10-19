class Element
{
    public string Name { get; set; }

    public Element(string name)
    {
        Name = name;
    }
}

class Program
{
    static string ConcatenateNames(List<Element> elements, char delimiter)
    {
        return string.Join(delimiter.ToString(), elements.Skip(3).Select(e => e.Name));
    }

    static void Main(string[] args)
    {
        List<Element> elements = new List<Element>
        {
            new Element("Алексей"),
            new Element("Маша"),
            new Element("Дмитрий"),
            new Element("Катя"),
            new Element("Саша"),
            new Element("Роман"),
            new Element("Светлана"),
        };

        char delimiter = '-';

        string result = ConcatenateNames(elements, delimiter);
        Console.WriteLine(result);
    }
}
