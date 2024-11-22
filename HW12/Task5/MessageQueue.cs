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
