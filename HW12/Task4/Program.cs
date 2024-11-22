class Program
{
    static void Main()
    {
        var bridge = new NarrowBridge();
        var random = new Random();

        for (int i = 1; i <= 10; i++)
        {
            int carId = i;
            string direction = random.Next(2) == 0 ? "слева направо" : "справа налево";

            new Thread(() => bridge.CrossBridge(direction, carId)).Start();
        }
    }
}
