public class FractionSimplifier
{
    public static string Simplify(string arg)
    {
        string[] parts = arg.Split('/');
        int numerator = int.Parse(parts[0]);
        int denominator = int.Parse(parts[1]);
        int gcd = GCD(numerator, denominator);
        numerator /= gcd;
        denominator /= gcd;
        if (denominator == 1)
        {
            return numerator.ToString();
        }
        return $"{numerator}/{denominator}";
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return Math.Abs(a);
    }

    public static void Main()
    {
        Console.WriteLine(Simplify("4/6"));    
        Console.WriteLine(Simplify("10/11"));   
        Console.WriteLine(Simplify("100/400"));
        Console.WriteLine(Simplify("8/4"));
    }
}
