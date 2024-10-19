class Program
{
    static List<string> Bucketize(string phrase, int n)
    {
        var words = phrase.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        List<string> buckets = new List<string>();
        string currentBucket = "";

        foreach (var word in words)
        {
            if (currentBucket.Length + word.Length + (currentBucket.Length > 0 ? 1 : 0) > n)
            {
                buckets.Add(currentBucket.Trim());
                currentBucket = "";
            }

            if (currentBucket.Length > 0)
            {
                currentBucket += " ";
            }
            currentBucket += word;
        }

        if (currentBucket.Length > 0)
        {
            buckets.Add(currentBucket.Trim());
        }

        return buckets;
    }

    static void PrintResult(List<string> buckets)
    {
        Console.WriteLine("[{0}]", string.Join(", ", buckets.Select(bucket => $"\"{bucket}\"")));
    }

    static void Main()
    {
        var result1 = Bucketize("она продает морские раковины у моря", 16);
        var result2 = Bucketize("мышь прыгнула через сыр", 8);
        var result3 = Bucketize("волшебная пыль покрыла воздух", 15);
        var result4 = Bucketize("a b c d e", 2);

        PrintResult(result1);
        PrintResult(result2);
        PrintResult(result3);
        PrintResult(result4);
    }
}
