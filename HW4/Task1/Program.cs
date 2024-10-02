public delegate double Function(double x);

class Program
{
    static double Integrate(Function f, double a, double b, int n = 1000)
    {
        double h = (b - a) / n;
        double sum = 0.5 * (f(a) + f(b));
        for (int i = 1; i < n; i++)
            sum += f(a + i * h);
        return sum * h;
    }

    static void Main(string[] args)
    {
        Function f1 = x => x * x;
        Function f2 = x => Math.Sin(x); 
        Function f3 = x => Math.Exp(x);   

        double integralF1 = Integrate(f1, 0, 1);
        double integralF2 = Integrate(f2, 0, Math.PI);
        double integralF3 = Integrate(f3, 0, 1);

        Console.WriteLine($"Интеграл x^2 от 0 до 1: {integralF1}");
        Console.WriteLine($"Интеграл sin(x) от 0 до pi: {integralF2}");
        Console.WriteLine($"Интеграл e^x от 0 до 1: {integralF3}");
    }
}

