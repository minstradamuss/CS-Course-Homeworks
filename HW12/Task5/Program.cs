class Program
{
    static async Task Producer(string name, MessageQueue<string> queue)
    {
        for (int i = 0; i < 10; i++)
        {
            string message = $"{name}: Message {i}";
            await queue.EnqueueAsync(message);
            Console.WriteLine($"{name} produced: {message}");
            await Task.Delay(500);
        }
    }

    static async Task Consumer(string name, MessageQueue<string> queue)
    {
        while (true)
        {
            var (success, message) = await queue.TryDequeueAsync(name);
            if (!success)
                break;

            Console.WriteLine($"{name} consumed: {message}");
            await Task.Delay(1000);
        }
    }

    static async Task Main(string[] args)
    {
        var messageQueue = new MessageQueue<string>();

        var producers = new List<Task>
        {
            Task.Run(() => Producer("Producer1", messageQueue)),
            Task.Run(() => Producer("Producer2", messageQueue))
        };

        var consumers = new List<Task>
        {
            Task.Run(() => Consumer("Consumer1", messageQueue)),
            Task.Run(() => Consumer("Consumer2", messageQueue))
        };

        await Task.WhenAll(producers);

        messageQueue.Stop();

        await Task.WhenAll(consumers);
    }
}
