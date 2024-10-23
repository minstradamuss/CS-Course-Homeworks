class Program
{
    static SemaphoreSlim semaphoreT1 = new SemaphoreSlim(1, 1);
    static SemaphoreSlim semaphoreT2 = new SemaphoreSlim(0, 1);

    static void Main(string[] args)
    {
        Thread thread1 = new Thread(PrintT1);
        Thread thread2 = new Thread(PrintT2);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }

    static void PrintT1()
    {
        for (int i = 1; i <= 10; i++)
        {
            semaphoreT1.Wait();
            Console.WriteLine($"thread_1: Строка {i}");
            semaphoreT2.Release();
        }
    }

    static void PrintT2()
    {
        for (int i = 1; i <= 10; i++)
        {
            semaphoreT2.Wait();
            Console.WriteLine($"thread_2: Строка {i}");
            semaphoreT1.Release();
        }
    }
}
