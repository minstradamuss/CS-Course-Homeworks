public class Hamster : IComparable<Hamster>
{
    public string Color { get; set; }
    public string FurType { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public int Age { get; set; }

    public Hamster(string color, string furType, double weight, double height, int age)
    {
        Color = color;
        FurType = furType;
        Weight = weight;
        Height = height;
        Age = age;
    }

    public double GetValue()
    {
        double colorValue = Color.Length;
        double furTypeValue = FurType.Length;
        // допустим такая формула для подсчета "ценности"
        return colorValue + furTypeValue + Weight * 10 + Height + Age;
    }

    public int CompareTo(Hamster other)
    {
        return other.GetValue().CompareTo(this.GetValue());
    }

    public override string ToString()
    {
        return $"Hamster [Color: {Color}, FurType: {FurType}, Weight: {Weight}kg, Height: {Height}cm, Age: {Age} months, Value: {GetValue()}]";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Random random = new Random();
        List<Hamster> hamsters = new List<Hamster>();
        string[] colors = { "White", "Black", "Brown", "Gray", "Golden" };
        string[] furTypes = { "Short", "Long", "Curly", "Smooth" };
        for (int i = 0; i < 10; i++)
        {
            string color = colors[random.Next(colors.Length)];
            string furType = furTypes[random.Next(furTypes.Length)];
            double weight = Math.Round(random.NextDouble() * 2 + 0.1, 2);
            double height = Math.Round(random.NextDouble() * 10 + 5, 2);
            int age = random.Next(1, 36);
            hamsters.Add(new Hamster(color, furType, weight, height, age));
        }
        Console.WriteLine("Initial list of hamsters:");
        hamsters.ForEach(Console.WriteLine);
        hamsters.Sort();
        Console.WriteLine("\nSorted list of hamsters by value:");
        hamsters.ForEach(Console.WriteLine);
    }
}
