class Program
{
    static long[] MaxMin(ulong number)
    {
        char[] digits = number.ToString().ToCharArray();

        char[] maxDigits = digits.OrderByDescending(c => c).ToArray();
        char[] minDigits = digits.OrderBy(c => c).ToArray();

        if (minDigits[0] == '0')
        {
            for (int i = 1; i < minDigits.Length; i++)
            {
                if (minDigits[i] != '0')
                {
                    char temp = minDigits[0];
                    minDigits[0] = minDigits[i];
                    minDigits[i] = temp;
                    break;
                }
            }
        }

        long maxResult = long.Parse(new string(maxDigits));
        long minResult = long.Parse(new string(minDigits));

        return new long[] { maxResult, minResult };
    }

    static void Main()
    {
        ulong[] testNumbers = { 12340, 98761, 9000, 11321 };

        foreach (ulong number in testNumbers)
        {
            Console.WriteLine($"maxmin({number}) -> [{string.Join(", ", MaxMin(number))}]");
        }
    }
}
