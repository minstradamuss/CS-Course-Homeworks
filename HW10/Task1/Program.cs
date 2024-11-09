class Program
{
    static int N = 5;
    static int X = 10;
    static int pot = 0;
    static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
    static Random random = new Random();
    static bool bearAwake = false;

    static async Task Main()
    {
        List<Task> beeTasks = new List<Task>();
        for (int i = 0; i < N; i++)
        {
            int beeId = i + 1;
            beeTasks.Add(Task.Run(() => Bee(beeId)));
        }

        Task bearTask = Task.Run(() => Bear());

        await Task.WhenAll(beeTasks);
        bearTask.Wait();
    }

    static async Task Bee(int id)
    {
        while (true)
        {
            await Task.Delay(random.Next(500, 2000));

            await semaphore.WaitAsync();

            if (pot < X)
            {
                pot++; 
                Console.WriteLine($"Пчела {id} принесла мед. Текущее количество порций: {pot}");

                if (pot == X)
                {
                    bearAwake = true;
                    Console.WriteLine("Горшок полон! Пчела {0} будит медведя.", id);
                }
            }

            semaphore.Release();

            while (bearAwake)
            {
                await Task.Delay(100);
            }
        }
    }

    static async Task Bear()
    {
        while (true)
        {
            while (!bearAwake)
            {
                await Task.Delay(100);
            }

            await semaphore.WaitAsync();

            Console.WriteLine("Медведь проснулся и съел весь мед!");
            pot = 0; 
            bearAwake = false;

            semaphore.Release(); 
        }
    }
}
