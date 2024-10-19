class Program
{
    static void Main()
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>
        {
            { "this", "эта" },
            { "dog", "собака" },
            { "eats", "ест" },
            { "too", "слишком" },
            { "much", "много" },
            { "vegetables", "овощей" },
            { "after", "после" },
            { "lunch", "обеда" }
        };

        string text = "This dog eats too much vegetables after lunch";
        int N = 3; 

        var words = text.Split(' ');

        var pages = words
            .Select(word => dictionary[word.ToLower()]) 
            .Select(word => word.ToUpper())     
            .Select((word, index) => new { Word = word, Index = index }) 
            .GroupBy(x => x.Index / N)  
            .Select(group => string.Join(" ", group.Select(x => x.Word))); 

        int pageNumber = 1;
        foreach (var page in pages)
        {
            Console.WriteLine($"{page}");
            pageNumber++;
        }
    }
}
