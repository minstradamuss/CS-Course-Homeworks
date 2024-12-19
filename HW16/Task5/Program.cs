using System.Text;

class Program
{
    public static string Rational(int numerator, int denominator)
    {
        StringBuilder result = new StringBuilder();
        result.Append("0.");

        Dictionary<int, int> remainderPositions = new Dictionary<int, int>();
        int remainder = numerator % denominator;

        while (remainder != 0)
        {
            if (remainderPositions.ContainsKey(remainder))
            {
                int repeatIndex = remainderPositions[remainder];
                result.Insert(repeatIndex, '(');
                result.Append(')');
                return result.ToString();
            }

            remainderPositions[remainder] = result.Length;
            remainder *= 10;
            result.Append(remainder / denominator);
            remainder %= denominator;
        }

        return result.ToString();
    }

    static void Main(string[] args)
    {
        var testCases = new (int numerator, int denominator, string expected)[]
        {
            (2, 5, "0.4"),
            (1, 6, "0.1(6)"),
            (1, 3, "0.(3)"),
            (1, 7, "0.(142857)"),
            (1, 77, "0.(012987)"),
            (1, 17, "0.(0588235294117647)"),
            (1, 19, "0.(052631578947368421)"),
            (1, 23, "0.(0434782608695652173913)")
        };

        bool allTestsPassed = true;

        foreach (var (numerator, denominator, expected) in testCases)
        {
            string result = Rational(numerator, denominator);
            if (result != expected)
            {
                Console.WriteLine($"Test failed for input ({numerator}, {denominator}). Expected: {expected}, Got: {result}");
                allTestsPassed = false;
            }
        }

        if (allTestsPassed)
        {
            Console.WriteLine("All test cases passed!");
        }
    }
}
