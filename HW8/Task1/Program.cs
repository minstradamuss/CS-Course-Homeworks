class Program
{
    private static readonly object lock1 = new object();
    private static readonly object lock2 = new object();

    static void Main(string[] args)
    {
        Thread thread1 = new Thread(Thread1Work);
        Thread thread2 = new Thread(Thread2Work);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Программа завершена.");
    }

    private static void Thread1Work()
    {
        lock (lock1)
        {
            Console.WriteLine("Поток 1 захватил lock1");
            Thread.Sleep(1000);

            lock (lock2)
            {
                Console.WriteLine("Поток 1 захватил lock2");
            }
        }
    }

    private static void Thread2Work()
    {
        lock (lock2)
        {
            Console.WriteLine("Поток 2 захватил lock2");
            Thread.Sleep(1000);

            lock (lock1)
            {
                Console.WriteLine("Поток 2 захватил lock1");
            }
        }
    }
}
