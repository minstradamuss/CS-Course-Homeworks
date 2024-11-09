namespace SleepingBarber
{
    public class Barbershop
    {
        private readonly object locker = new();
        private readonly int size;
        private readonly Queue<Client> queue = new();
        private readonly Task task;

        public Barbershop(int queueSize)
        {
            size = queueSize;
            task = Task.Run(WorkingCycle);
        }

        public void AddClient(Client client)
        {
            lock (locker)
            {
                if (queue.Count >= size)
                {
                    Console.WriteLine($"{client} ушел: все кресла заняты.");
                    return;
                }

                Console.WriteLine($"{client} сел в кресло для ожидающих.");
                queue.Enqueue(client);
                Monitor.Pulse(locker);
            }
        }

        private void WorkingCycle()
        {
            while (true)
            {
                Client client;
                lock (locker)
                {
                    while (queue.Count == 0)
                        Monitor.Wait(locker);
                    client = queue.Dequeue();
                }
                Console.WriteLine($"{client} сел в кресло для стрижки.");
                Thread.Sleep(client.ProcessingTime);
                Console.WriteLine($"{client} постригли и он ушел.");
            }
        }

        public void Wait() => task.Wait();
    }

    public record Client(int Id, int ProcessingTime)
    {
        public override string ToString() => $"Посетитель {Id}";
    }

    class Program
    {
        static void SimulateRandom()
        {
            var rand = new Random();
            int clientCount = rand.Next(10, 20);
            int queueSize = rand.Next(1, 5);

            Console.WriteLine($"{clientCount} посетителей пришло, кресел в парикмахерской {queueSize}.");

            var barbershop = new Barbershop(queueSize);
            for (int i = 1; i <= clientCount; i++)
            {
                Thread.Sleep(rand.Next(100, 2000));
                barbershop.AddClient(new Client(i, rand.Next(500, 5000)));
            }
            barbershop.Wait();
        }

        static void Main()
        {
            SimulateRandom();
        }
    }
}
