class HungryChicks
{
    private static int F = 10; 
    private static int portions = F; 
    private static SemaphoreSlim foodSemaphore = new SemaphoreSlim(F, F);
    private static object lockObj = new object(); 

    static void Main()
    {
        int N = 5; 
        Thread[] chicks = new Thread[N];

        for (int i = 0; i < N; i++)
        {
            int chickId = i;
            chicks[i] = new Thread(() => ChickBehavior(chickId));
            chicks[i].Start();
        }

        foreach (Thread chick in chicks)
        {
            chick.Join();
        }
    }

    static void ChickBehavior(int chickId)
    {
        Random random = new Random();
        while (true)
        {
            foodSemaphore.Wait();
            lock (lockObj)
            {
                portions--;
                Console.WriteLine($"Птенец {chickId} съел порцию. Осталось {portions} порций.");

                if (portions == 0)
                {
                    Console.WriteLine($"Миска опустела. Птенец {chickId} зовет мать.");
                    MotherRefillsBowl();
                }
            }

            Thread.Sleep(random.Next(500, 2000));
        }
    }

    static void MotherRefillsBowl()
    {
        Thread.Sleep(1000);
        lock (lockObj)
        {
            portions = F;
            foodSemaphore.Release(F);
            Console.WriteLine("Мать наполнила миску.");
        }
    }
}
