using NCar;
using NHorse;

namespace Task4
{
    public class Program
    {
        public static void Main()
        {
            Horse horse1 = new Horse("Draft", 800, 160, 5);
            Horse horse2 = new Horse("Race", 500, 150, 3);

            Car carFromHorse1 = horse1;
            Car carFromHorse2 = horse2;

            Console.WriteLine($"Horse1 as Car: {carFromHorse1}");
            Console.WriteLine($"Horse2 as Car: {carFromHorse2}");

            Console.WriteLine($"Horse1 > Horse2: {horse1 > horse2}");
            Console.WriteLine($"Horse1 < Horse2: {horse1 < horse2}");
            Console.WriteLine($"Horse1 == Horse2: {horse1 == horse2}");
        }
    }
}