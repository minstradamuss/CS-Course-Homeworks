class Program
{
    private int outputNum;
    private static readonly Random random = new Random();

    private void Delay(double probability = 0.6, int delayDuration = 100)
    {
        double chance = random.NextDouble();
        if (chance < probability)
        {
            Thread.Sleep(delayDuration);
        }
    }

    private void Set(int value)
    {
        Delay();
        if (outputNum == 0)
        {
            Delay();
            outputNum = value;
        }
    }

    public void CreateThreads()
    {
        for (int i = 0; i < 20; i++)
        {
            outputNum = 0;
            Thread thread1 = new Thread(() => Set(2));
            Thread tread2 = new Thread(() => Set(3));

            thread1.Start();
            tread2.Start();

            thread1.Join();
            tread2.Join();

            Console.WriteLine($"x = {outputNum}");
        }
    }

    public static void Main()
    {
        Program program = new Program();
        program.CreateThreads();
    }
}
