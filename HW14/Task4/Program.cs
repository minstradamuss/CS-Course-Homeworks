class Program
{
    public static string MergeStrings(string s, string t)
    {
        string[] wordS = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] wordT = t.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int[,] dp = new int[wordS.Length + 1, wordT.Length + 1];

        for (int i = 1; i <= wordS.Length; i++)
        {
            for (int j = 1; j <= wordT.Length; j++)
            {
                if (wordS[i - 1] == wordT[j - 1])
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                else
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
            }
        }

        List<string> result = new();
        int x = wordS.Length, y = wordT.Length;

        while (x > 0 || y > 0)
        {
            if (x > 0 && y > 0 && wordS[x - 1] == wordT[y - 1])
            {
                result.Add(wordS[x - 1]);
                x--;
                y--;
            }
            else if (y > 0 && (x == 0 || dp[x, y - 1] >= dp[x - 1, y]))
            {
                result.Add(wordT[y - 1]);
                y--;
            }
            else if (x > 0)
            {
                result.Add(wordS[x - 1]);
                x--;
            }
        }

        result.Reverse();
        return string.Join(' ', result);
    }

    static void Main(string[] args)
    {
        string str1 = "Шла Маша по шоссе пешком";
        string str2 = "Шла Саша по горе";

        Console.WriteLine("Строка 1:");
        Console.WriteLine(str1);
        Console.WriteLine("Строка 2:");
        Console.WriteLine(str2);
        string mergedResult = MergeStrings(str1, str2);

        Console.WriteLine("Результат слияния строк:");
        Console.WriteLine(mergedResult);
    }
}
