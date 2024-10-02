class Program
{
    static string ExpressFactors(int n)
    {
        Dictionary<int, int> primeFactors = GetPrimeFactors(n);
        List<string> result = new List<string>();
        foreach (var factor in primeFactors.OrderBy(kv => kv.Key))
        {
            if (factor.Value == 1)
                result.Add(factor.Key.ToString());
            else
                result.Add($"{factor.Key}^{factor.Value}");
        }
        return string.Join(" x ", result);
    }

    static Dictionary<int, int> GetPrimeFactors(int n)
    {
        Dictionary<int, int> factors = new Dictionary<int, int>();
        for (int i = 2; i * i <= n; i++)
        {
            while (n % i == 0)
            {
                if (factors.ContainsKey(i))
                    factors[i]++;
                else
                    factors[i] = 1;
                n /= i;
            }
        }
        if (n > 1)
            factors[n] = 1;
        return factors;
    }

    static void Main(string[] args)
    {
        Console.WriteLine(ExpressFactors(2));   
        Console.WriteLine(ExpressFactors(4));  
        Console.WriteLine(ExpressFactors(10));  
        Console.WriteLine(ExpressFactors(60));  
    }
}
