class Program
{
    static long LuckyTicket(int n)
    {
        if (n % 2 != 0) throw new ArgumentException("n должно быть четным");
        int half = n / 2;
        int maxSum = 9 * half;
        long[] count = new long[maxSum + 1];
        count[0] = 1;
        for (int i = 0; i < half; i++)
        {
            long[] newCount = new long[maxSum + 1];
            for (int j = 0; j <= maxSum; j++)
            {
                for (int digit = 0; digit <= 9; digit++)
                {
                    if (j + digit <= maxSum)
                    {
                        newCount[j + digit] += count[j];
                    }
                }
            }
            count = newCount;
        }
        long total = 0;
        for (int i = 0; i <= maxSum; i++)
        {
            total += count[i] * count[i];
        }

        return total;
    }

    static void Main()
    {
        Console.WriteLine(LuckyTicket(2));   
        Console.WriteLine(LuckyTicket(4));    
        Console.WriteLine(LuckyTicket(12));    
    }
}
