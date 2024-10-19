using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string sentence = "Это что же получается: ходишь, ходишь в школу, а потом бац - вторая смена";

        var words = Regex.Replace(sentence, @"[^\w\s]", "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        var groupedWords = words
            .GroupBy(word => word.Length)
            .Select(group => new
            {
                Length = group.Key,
                Count = group.Count(),
                Words = group.ToList()
            })
            .OrderByDescending(g => g.Count)
            .ThenByDescending(g => g.Length)
            .ToList();

        for (int i = 0; i < groupedWords.Count; i++)
        {
            var group = groupedWords[i];
            Console.WriteLine($"Группа {i + 1}. Длина {group.Length}. Количество {group.Count}");
            Console.WriteLine(string.Join(Environment.NewLine, group.Words));
            Console.WriteLine();
        }
    }
}
