using System.Collections.Concurrent;

public class MessageQueue<T>
{
    private readonly BlockingCollection<(T Message, HashSet<string> PendingConsumers)> _queue = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly HashSet<string> _consumers = new();
    private bool _isRunning = true;

    public async Task EnqueueAsync(T message)
    {
        if (!_isRunning)
            throw new InvalidOperationException("Queue is stopped. Cannot add more messages.");

        await _semaphore.WaitAsync();
        try
        {
            _queue.Add((message, new HashSet<string>(_consumers)));
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<(bool Success, T Message)> TryDequeueAsync(string consumerName)
    {
        if (!_isRunning && _queue.IsCompleted)
            return (false, default);

        T message = default;

        await _semaphore.WaitAsync();
        try
        {
            _consumers.Add(consumerName);
        }
        finally
        {
            _semaphore.Release();
        }

        foreach (var (msg, pendingConsumers) in _queue)
        {
            if (pendingConsumers.Remove(consumerName))
            {
                message = msg;

                if (pendingConsumers.Count == 0)
                    _queue.TryTake(out _);

                return (true, message);
            }
        }

        return (false, default);
    }

    public void Stop()
    {
        _isRunning = false;
        _queue.CompleteAdding();
    }
}

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
